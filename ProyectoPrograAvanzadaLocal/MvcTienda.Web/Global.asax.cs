using MvcTienda.Domain.Entities;
using MvcTienda.Infrastructura.Data;
using MvcTienda.Infrastructura.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcTienda.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();


            SeedIdentity();
        }
        private void SeedIdentity()
        {
            using (var context = new AppDbContext())
            {
                
                if (!context.Estados.Any())
                {
                    context.Estados.Add(new Estado { Nombre = "Activo" });
                    context.Estados.Add(new Estado { Nombre = "Inactivo" });
                    context.Estados.Add(new Estado { Nombre = "Pendiente" });
                    context.Estados.Add(new Estado { Nombre = "Aprobado" });
                    context.Estados.Add(new Estado { Nombre = "Rechazado" });
                    context.SaveChanges();
                }

                var userStore = new ApplicationUserStore(context);
                var roleStore = new ApplicationRoleStore(context);

                var userManager = new ApplicationUserManager(userStore);
                var roleManager = new ApplicationRoleManager(roleStore);

               
                IdentitySeeder.Seed(roleManager, userManager).Wait();
            }
        }
    }
}
