using Issuer.Service.Data;
using Issuer.Service.Repository;
using Issuer.Service.Repository.Implementations;
using Issuer.Service.Repository.Interfaces;
using Issuer.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Issuer.Service
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
            services.AddDbContext<IssuerDbContext>(x =>
                    x.UseLazyLoadingProxies().
                    UseSqlServer(Configuration.GetConnectionString("DbConnection")));
            services.AddControllers().AddNewtonsoftJson();
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy1",
                    corsBuilder => corsBuilder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyOrigin());
            });

            services.AddScoped<IRestClient, RestClient>();
            services.AddScoped<IGenericRestClient, GenericRestClient>();
            services.AddScoped<IssuerDbContext>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IIssuerPaymentService, IssuerPaymentService>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddTransient<Seed>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seed seed)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors("CorsPolicy1");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            seed.SeedData();

        }
    }
}
