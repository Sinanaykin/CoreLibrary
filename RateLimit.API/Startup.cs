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

            services.AddOptions();//appsetting içinden okumak için  bu kütüphaneyi ekledik.
            services.AddMemoryCache();//Bu kütüphane ile uygulamada endpointe ne kadar istek atýlmýþ bunu bulucaz.Bu deðerleri cashe de tutuyor ordan okucaz.
            services.Configure<IpRateLimitOptions>(Configuration.GetSection
                ("IpRateLimiting"));//Bu ipde bu kadar istek yapabilir diyebileceðim kütüphane ekledik appsettingden okuyacak getsection ile

            services.Configure<IpRateLimitPolicies>(Configuration.GetSection
                ("IpRateLimitPolicies"));//ip adreslerine farklý þartlar atayabilmek için bunu ekledik.Getsection appsetting de ki isimleri

           
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();//Api adresindeki datalarý policies içindeki datalarý tutacaðý memory cache i belirtiyoruz.Addsingleton dedik çünkü uygulama ayaða kalkýnca bir kere nesne oluþsun bir daha gerek yok zaten .IIpPolicyStore ile karþýlaþýrsan git MemoryCacheIpPolicyStore e kaydet diyoruz.Bir tane instance ayaga kalkýyorsa Memorycache kulanabiliiriz ama  docker üzerinden  mesela 5 tane container ayaða kalkýyorsa þartlarý merkezi memory de tutmalýyýz bunun içinde distrubute cache(DistributedCacheIpPolicyStore) kullanmalýyýz Redis gibi örneðin.

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();//request  sayýsýný tutan yapý(IRateLimitCounterStore) yine memoryde tutucaz

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //reuesti yapanýn ip adresini , header bilgisini okumak için bunu kullanmalýyýz.
             // IHttpContextAccessor ile karsýlasýrsam  HttpContextAccessor classý üzerinden bir nesne örneði al


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
