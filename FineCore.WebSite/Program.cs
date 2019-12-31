using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FineCore.WebSite {
    public class Program {
        public static void Main(string[] args) {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.ConfigureLogging(
                //    (context, loggerBuilder) => {
                //        loggerBuilder.AddFilter("System", LogLevel.Warning);
                //        loggerBuilder.AddFilter("Microsoft", LogLevel.Warning);
                //        loggerBuilder.AddLog4Net();
                //    })
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
