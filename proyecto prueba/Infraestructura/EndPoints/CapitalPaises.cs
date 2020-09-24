using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace proyecto_prueba.Infraestructura.EndPoints
{
    //......clase que va a definer metodo a invocar por un ENDPOINT........
    public static class CapitalPaises
    {
        public static async Task EndPoint(HttpContext contexto)
        {
            String pais = contexto.Request.RouteValues["pais"] as String ?? "España";
            //?? valor por defecto igual a:
            /*if (pais ==  null){
            {
            pais=España
            }
            */
            String capital = "";
            switch (pais.ToLower())
            {
                case "españa":
                    capital = "Madrid";
                    break;
                case "francia":
                    capital = "Paris";
                    break;                    
            }
            await contexto.Response.WriteAsync($"El pasi: {pais} tiene de capital: {capital}");
        }
    }
}
