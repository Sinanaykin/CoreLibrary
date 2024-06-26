using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateLimitt.API
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
            services.AddOptions();//appsetting i�inden okumak i�in  bu k�t�phaneyi ekledik.
            services.AddMemoryCache();//Bu k�t�phane ile uygulamada endpointe ne kadar istek at�lm�� bunu bulucaz.Bu de�erleri cashe de tutuyor ordan okucaz.


            //Not Client Id Ratelimit yapmak i�in bunu kapad�k �imdilik
            //services.Configure<IpRateLimitOptions>(Configuration.GetSection
            //    ("IpRateLimiting"));//Bu ipde bu kadar istek yapabilir diyebilece�im k�t�phane ekledik appsettingden okuyacak getsection ile
            services.Configure<ClientRateLimitOptions>(Configuration.GetSection
              ("ClientRateLimiting"));//Bu ipde bu kadar istek yapabilir diyebilece�im k�t�phane ekledik appsettingden okuyacak getsection ile

            //Not Client Id Ratelimit yapmak i�in bunu kapad�k �imdilik
            //services.Configure<IpRateLimitPolicies>(Configuration.GetSection
            //    ("IpRateLimitPolicies"));//ip adreslerine farkl� �artlar atayabilmek i�in bunu ekledik.Getsection appsetting de ki isimleri
            services.Configure<ClientRateLimitPolicies>(Configuration.GetSection
                ("ClientRateLimitPolicies"));//ip adreslerine farkl� �artlar atayabilmek i�in bunu ekledik.Getsection appsetting de ki isimleri

            //Not Client Id Ratelimit yapmak i�in bunu kapad�k �imdilik
            //services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();//Api adresindeki datalar� policies i�indeki datalar� tutaca�� memory cache i belirtiyoruz.Addsingleton dedik ��nk� uygulama aya�a kalk�nca bir kere nesne olu�sun bir daha gerek yok zaten .IIpPolicyStore ile kar��la��rsan git MemoryCacheIpPolicyStore e kaydet diyoruz.Bir tane instance ayaga kalk�yorsa Memorycache kulanabiliiriz ama  docker �zerinden  mesela 5 tane container aya�a kalk�yorsa �artlar� merkezi memory de tutmal�y�z bunun i�inde distrubute cache(DistributedCacheIpPolicyStore) kullanmal�y�z Redis gibi �rne�in.
            services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();


            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();//request  say�s�n� tutan yap�(IRateLimitCounterStore) yine memoryde tutucaz

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //reuesti yapan�n ip adresini , header bilgisini okumak i�in bunu kullanmal�y�z.
                                                                                // IHttpContextAccessor ile kars�las�rsam  HttpContextAccessor class� �zerinden bir nesne �rne�i al

            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();//Ana servisimiz.IRateLimitConfiguration ile kars�las�rsam  RateLimitConfiguration class� �zerinden bir nesne �rne�i al
            services.AddControllers();
         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Bunu kapad�k client �d rate limit kullanmak i�in �im�dilik
            //app.UseIpRateLimiting();//middleware buraya ekliyoruz

            app.UseClientRateLimiting();//Client�d rate limit i�in middle ware eklememiz laz�m


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
