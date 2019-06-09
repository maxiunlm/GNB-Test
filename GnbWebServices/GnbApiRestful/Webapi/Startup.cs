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
using Service;
using Business;
using Data;
using System.IO;
using NLog;
using NLog.Extensions.Logging;

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
            // Add Cors
            services.AddCors(o => o.AddPolicy(myPolicy, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddHttpClient();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            ConfigureIoCApp(services);
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
            services.AddTransient<ISkuData, SkuData>();
            services.AddTransient<IRateData, RateData>();
            services.AddTransient<ITransactionData, TransactionData>();
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
