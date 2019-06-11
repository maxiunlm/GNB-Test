using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Webapi.Controllers;
using Webapi.Profiles;
using Webapi.Filters;
using Service;
using Business;
using Data;
using System.IO;
using NLog;
using NLog.Extensions.Logging;
using AutoMapper;

namespace Webapi
{
    public class Startup
    {
        const string myPolicy = "MyPolicy";

        public Startup(IConfiguration configuration)
        {
            string logConfigPath = string.Concat(Directory.GetCurrentDirectory(), "/nlog.config");
            LogManager.LoadConfiguration(logConfigPath);

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            ConfigureFilters(services);
            ConfigureMapping(services);
            ConfigureIoCApp(services);
            ConfigureCors(services);
        }

        private void ConfigureMapping(IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private void ConfigureFilters(IServiceCollection services)
        {
            // Auditoria - Filters - Console Output
            services.AddMvc(o =>
            {
                o.Filters.Add(typeof(WebExceptionFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<WebExceptionFilter>();
            services.AddScoped<WebLoggerFilter>();
        }

        private void ConfigureIoCApp(IServiceCollection services)
        {
            // IoC - Transient: A new instance Per Call
            services.AddTransient<ISkuService, SkuService>();
            services.AddTransient<IRateService, RateService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<ISkuBusiness, SkuBusiness>();
            services.AddTransient<IRateBusiness, RateBusiness>();
            services.AddTransient<ITransactionBusiness, TransactionBusiness>();
            services.AddTransient<IBankCalculus, BankCalculus>();
            services.AddTransient<ISkuData, SkuData>();
            services.AddTransient<IRateData, RateData>();
            services.AddTransient<ITransactionData, TransactionData>();
        }

        private void ConfigureCors(IServiceCollection services)
        {
            // Permite que la Api se publica o para un grupo determinado de clientes
            services.AddCors(o => o.AddPolicy(myPolicy, builder =>
            {
                // // Se puede configurar los endpoint clientes aqui
                // builder.WithOrigins("http://localhost:300")
                //         .WithMethods("GET")
                //         .WithHeaders("name");
                // o se puede abrir a cualquier cliente
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                // .AllowCredentials(); // No es necesario en este Ejemplo
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Enable Cors
            app.UseCors(myPolicy);

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
