using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logging
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().ConfigureLogging(logging =>
                    {

                        logging.ClearProviders ();
                    }) ;
                });
        //public static void Main(string[] args)
        //{

        //    var host = CreateHostBuilder(args).Build();
        //    var logger = host.Services.GetRequiredService<ILogger<Program>>();//program.cs log koymak için bu yapýlýr.çünkü burada dependency ýnjection yapamayýz burda böyle loglanýr
        //    logger.LogInformation("Uygulama ayaga kalkýyor");
        //}

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}
