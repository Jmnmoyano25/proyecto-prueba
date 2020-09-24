using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using proyecto_prueba.Infraestructura.EndPoints;

namespace proyecto_prueba
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

            //-----middleware que detecta que endpoint va a ejecutar el modulo de enrutamiento-----
            //-----ponerlo siempre tras el modulo app.UseRouting().......

            //el metodo USE que define el middleware nedcesita como parametro una función LAMBDA, con dos pararametros,
            //el 1º objeto Httpcontext,
            //el 2º RequestDele
            app.Use(async (contexto, next) => {
                Endpoint end = contexto.GetEndpoint();
                if (end != null)
                {
                    await contexto
                          .Response
                          .WriteAsync($"nngún endpoint selecónado, no se cumple ningún patron");
                }

                await next(); //invoca a siguiente modulo del middleware
            }); 


            app.UseEndpoints(endpoints =>
            {
                //........1º endpoint (endpoint por defecto)
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}")
                    .WithDisplayName("endpoint por defecto");

                //........2º endpoint
                endpoints.MapGet("/capitalPais/{pais}", CapitalPaises.EndPoint);
                //es mas restrictivo por que tiene un segmento literal (/capitalPais
            });
        }
    }
}
