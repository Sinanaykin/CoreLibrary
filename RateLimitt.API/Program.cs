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
            //appsetting deki verileri okumak i�in bu yap�y� kurduk .
           //gidip appsetting i�ine bak�yor program il ayaga kalkt�g�nda
         var webPost= CreateHostBuilder(args).Build();

            //Bunlar�da kapad�k client  id rate limit kullanmak i�in
            //var IpPolicy = webPost.Services.GetRequiredService<IIpPolicyStore>();
            //IpPolicy.SeedAsync().Wait();//appsetting i�indeki kurallar� uygulayacak  seed metodu

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
