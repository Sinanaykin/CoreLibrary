using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smidge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmidgeApp.Web
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
            services.AddSmidge(Configuration.GetSection("smidge"));//Smidge k�t�phanesini �a��rd�k GetSection ile isim verdik appsetting den �a��rmak i�in.

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSmidge(bundle=>//Hangi dosyalar� bundle etmek istiyorsak bunu belirticez.
            {

               //bundle.CreateJs("my-js-bundle", "~/js/site.js", "~/js/site2.js");//my-js-bundle bu isimde bir js olu�turucak i�inde de ~/js/site.js,~/js/site2.js dosyalar� olacak.

                bundle.CreateJs("my-js-bundle", "~/js/");//Burada mesela t�m js ler ayn� yerde(klas�rde) bu y�zden yukar�daki gibi tek tek belirtmeye gerek yok burdak�i gibi belirtsek de olur

                bundle.CreateCss("my-css-bundle", "~/css/site.css", "~/lib/bootstrap/dist/css/bootstrap.css");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
