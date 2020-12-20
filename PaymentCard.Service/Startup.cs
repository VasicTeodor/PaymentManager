using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Service.Data;
using Bank.Service.Repositories;
using Bank.Service.Repositories.Implementations;
using Bank.Service.Repositories.Interfaces;
using Bank.Service.Services;
using HealthChecks.UI.Client;
using Infrastructure.Service;
using Infrastructure.Service.Interface;
using Infrastructure.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
            //services.AddConsulConfig(Configuration);
            services.AddControllers();
            services.AddHealthChecks();
            services.AddHealthChecksUI().AddInMemoryStorage();

            // Register Services
            services.AddScoped<IRestClient, RestClient>();
            services.AddScoped<IGenericRestClient, GenericRestClient>();
            services.AddScoped<BankDbContext>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPaymentService, PaymentService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseConsul("bank");
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHealthChecks("/bank/checks/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI();
        }
    }
}
