using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GrapeCity.Documents.Excel;

namespace diodocs
{
    public class Startup
    {
        private readonly NLog.Logger logger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            logger = NLog.LogManager.GetCurrentClassLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Factory.IWorkbookFactory, Factory.WorkbookFactory>();
            services.AddSingleton<Services.IPrintManager, Services.PrintManagerWrapper>();
            services.AddSingleton<Services.IConverter, Services.Converter>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                if (Environment.GetEnvironmentVariable("APIKEY") != null)
                {
                    Workbook.SetLicenseKey(Environment.GetEnvironmentVariable("APIKEY"));
                }
                else
                {
                    logger.Info("There is no APIKEY in environment variable");
                }
            }
            else
            {
                Workbook.SetLicenseKey(Environment.GetEnvironmentVariable("APIKEY"));
            }

            // Do not use http redirection.
            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}