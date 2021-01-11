using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
using PaymentCardCentre.Service.Data;
using PaymentCardCentre.Service.Helpers;
using PaymentCardCentre.Service.Repository;
using PaymentCardCentre.Service.Services;
using RestSharp;

namespace PaymentCardCentre.Service
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
            services.AddDbContext<PCCDbContext>(x =>
                    x.UseSqlServer(Configuration.GetConnectionString("DbConnection2")));
            //services.AddConsulConfig(Configuration);
            services.AddControllers().AddNewtonsoftJson();
            services.AddHealthChecks();
            services.AddHealthChecksUI().AddInMemoryStorage();
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy1",
                    //corsBuilder => corsBuilder.WithOrigins("http://localhost:10662")
                    corsBuilder => corsBuilder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyOrigin());
            });

            // Register Services
            services.AddScoped<IRestClient, RestClient>();
            services.AddScoped<IGenericRestClient, GenericRestClient>();
            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IPCCPayment, PCCPayment>();
            services.AddScoped<PCCDbContext>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfiles());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Register Seed Class
            services.AddTransient<DbSeed>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbSeed seed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseConsul("pcc");
            app.UseRouting();
            app.UseCors("CorsPolicy1");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            seed.SeedBank();

            app.UseHealthChecks("/pcc/checks/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI();
        }
    }
}
