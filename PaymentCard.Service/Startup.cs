using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bank.Service.Data;
using Bank.Service.Helpers;
using Bank.Service.Repositories;
using Bank.Service.Repositories.Implementations;
using Bank.Service.Repositories.Interfaces;
using Bank.Service.Services;
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

namespace Bank.Service
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
            services.AddDbContext<BankDbContext>(x =>
                x.UseLazyLoadingProxies().
                UseSqlServer(Configuration.GetConnectionString("DbConnection")));

            services.AddConsulConfig(Configuration);
            services.AddControllers().AddNewtonsoftJson();
            services.AddHealthChecks();
            services.AddHealthChecksUI().AddInMemoryStorage();

            services.AddCors(options => {
                options.AddPolicy("CorsPolicy1",
                    //corsBuilder => corsBuilder.WithOrigins("http://localhost:4200", "http://localhost:4201", "http://localhost:3000", "http://localhost:5080")
                    corsBuilder => corsBuilder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyOrigin());
            });

            // Register Services
            services.AddScoped<IRestClient, RestClient>();
            services.AddScoped<IGenericRestClient, GenericRestClient>();
            services.AddScoped<BankDbContext>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IIssuerPaymentService, IssuerPaymentService>();
            services.AddScoped<IRegisterSellerService, RegisterSellerService>();

            // Register Mapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfiles());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Register Seed Class
            services.AddTransient<Seed>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seed seed)
        {
            //using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetRequiredService<BankDbContext>();
            //    context.Database.Migrate();
            //}

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseConsul("bank");
            app.UseRouting();
            app.UseCors("CorsPolicy1");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            seed.SeedData();

            app.UseHealthChecks("/bank/checks/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI();
        }
    }
}
