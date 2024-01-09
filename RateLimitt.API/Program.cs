using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AspNetCoreRateLimit;

namespace RateLimitt.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //appsetting deki verileri okumak için bu yapýyý kurduk .
           //gidip appsetting içine bakýyor program il ayaga kalktýgýnda
         var webPost= CreateHostBuilder(args).Build();

            //Bunlarýda kapadýk client  id rate limit kullanmak için
            //var IpPolicy = webPost.Services.GetRequiredService<IIpPolicyStore>();
            //IpPolicy.SeedAsync().Wait();//appsetting içindeki kurallarý uygulayacak  seed metodu

            webPost.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
