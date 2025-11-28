using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MvcTienda.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de Web API

            var cors = new System.Web.Http.Cors.EnableCorsAttribute(origins:"*",headers: "*",methods: "*");

            config.EnableCors(cors);

            // Rutas de Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
