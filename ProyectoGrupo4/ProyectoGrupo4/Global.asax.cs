using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProyectoGrupo4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProyectoGrupo4
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CrearRoles();
        }

        private void CrearRoles()
        {
            System.Diagnostics.Debug.WriteLine(">>> CrearRoles SE EJECUTA <<<");

            using (var context = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (!roleManager.RoleExists("Administrador"))
                {
                    var result = roleManager.Create(new IdentityRole("Administrador"));
                    System.Diagnostics.Debug.WriteLine("Rol Admin creado: " + result.Succeeded);
                }

                if (!roleManager.RoleExists("Asociado"))
                {
                    var result = roleManager.Create(new IdentityRole("Asociado"));
                    System.Diagnostics.Debug.WriteLine("Rol Asociado creado: " + result.Succeeded);
                }
            }
        }
    }
}
