using MvcTienda.Infrastructura.Data;
using MvcTienda.Infrastructura.Identity;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcTienda.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MvcTienda.Infrastructura.Data.AppDbInitalizer());
            AreaRegistration.RegisterAllAreas();

            UnityConfig.RegisterComponents();

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
                var userStore = new ApplicationUserStore(context);
                var roleStore = new ApplicationRoleStore(context);

                var userManager = new ApplicationUserManager(userStore);
                var roleManager = new ApplicationRoleManager(roleStore);

                IdentitySeeder.Seed(roleManager, userManager).Wait();
            }
        }
    }
}
