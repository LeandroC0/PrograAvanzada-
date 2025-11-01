using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
namespace Usuario
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            InitializeRoles();
        }

        private void InitializeRoles()
        {
            using (var context = new Models.UsuariosContext())
            {
                if (context.Roles.Find(1) == null)
                {
                    var adminRole = new Models.Rol
                    {
                        Rol_ID = 1,
                        Nombre = "Admin"
                    };
                    context.Roles.Add(adminRole);
                }
                if (context.Roles.Find(2) == null)
                {
                    var userRole = new Models.Rol
                    {
                        Rol_ID = 2,
                        Nombre = "User"
                    };
                    context.Roles.Add(userRole);
                }
                context.SaveChanges();
            }
        }
    }
}
