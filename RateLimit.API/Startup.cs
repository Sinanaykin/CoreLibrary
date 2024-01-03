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
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateLimit.API
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
            services.Configure<IpRateLimitOptions>(Configuration.GetSection
                ("IpRateLimiting"));//Bu ipde bu kadar istek yapabilir diyebilece�im k�t�phane ekledik appsettingden okuyacak getsection ile

            services.Configure<IpRateLimitPolicies>(Configuration.GetSection
                ("IpRateLimitPolicies"));//ip adreslerine farkl� �artlar atayabilmek i�in bunu ekledik.Getsection appsetting de ki isimleri

           
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();//Api adresindeki datalar� policies i�indeki datalar� tutaca�� memory cache i belirtiyoruz.Addsingleton dedik ��nk� uygulama aya�a kalk�nca bir kere nesne olu�sun bir daha gerek yok zaten .IIpPolicyStore ile kar��la��rsan git MemoryCacheIpPolicyStore e kaydet diyoruz.Bir tane instance ayaga kalk�yorsa Memorycache kulanabiliiriz ama  docker �zerinden  mesela 5 tane container aya�a kalk�yorsa �artlar� merkezi memory de tutmal�y�z bunun i�inde distrubute cache(DistributedCacheIpPolicyStore) kullanmal�y�z Redis gibi �rne�in.

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();//request  say�s�n� tutan yap�(IRateLimitCounterStore) yine memoryde tutucaz

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //reuesti yapan�n ip adresini , header bilgisini okumak i�in bunu kullanmal�y�z.
             // IHttpContextAccessor ile kars�las�rsam  HttpContextAccessor class� �zerinden bir nesne �rne�i al


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RateLimit.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RateLimit.API v1"));
            }
            app.UseIpRateLimiting();//Middleware imizi ekliyoruz
            
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
