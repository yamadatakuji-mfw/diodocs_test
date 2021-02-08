using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace diodocs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Run before CreateHostBuilder()

            NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration("config/nlog.config");
            var logger = NLog.LogManager.GetCurrentClassLogger();
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw ex;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {

            var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
            var url = String.Concat("http://0.0.0.0:", port);

            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseUrls(url);
                }).ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);
                }).UseNLog();
        }
    }
}
