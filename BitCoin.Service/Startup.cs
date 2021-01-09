using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BitCoin.Service.Helpers;
using BitCoin.Service.Models;
using BitCoin.Service.Repository;
using BitCoin.Service.Repository.Interfaces;
using BitCoin.Service.Services;
using BitCoin.Service.Services.Interfaces;
using HealthChecks.UI.Client;
using Infrastructure.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace BitCoin.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BitCoinContext>(x =>
                x.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

            services.AddConsulConfig(Configuration);
            services.AddControllers();
            services.AddHealthChecks();
            services.AddHealthChecksUI().AddInMemoryStorage();

            // CORS Policy
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
                    corsBuilder => corsBuilder.WithOrigins("http://localhost:4200", "http://localhost:3000", "https://www.sandbox.coingate.com")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyOrigin());
            });

            // Register domain services and repositories
            services.AddScoped<ICoingateService, CoingateService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IRestClient, RestClient>();
            services.AddScoped<IGenericRestClient, GenericRestClient>();

            // Register Mapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseConsul("bitcoin");
            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHealthChecks("/bitcoin/checks/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI();
        }
    }
}
