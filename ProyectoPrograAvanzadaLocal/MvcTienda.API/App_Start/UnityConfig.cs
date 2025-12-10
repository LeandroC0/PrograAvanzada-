using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MvcTienda.Aplicacion.Dashboard;
using MvcTienda.Aplicacion.DetallesOrden;
using MvcTienda.Aplicacion.Estados;
using MvcTienda.Aplicacion.Imagenes;
using MvcTienda.Aplicacion.Ordenes;
using MvcTienda.Aplicacion.Productos;
using MvcTienda.Aplicacion.Resennas;
using MvcTienda.Aplicacion.Seguridad;
using MvcTienda.Auth.Services;
using MvcTienda.Domain.Repositories;
using MvcTienda.Infrastructura.Identity;
using MvcTienda.Infrastructura.Data;
using MvcTienda.Infrastructura.Repositories;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace MvcTienda.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // DbContext
            container.RegisterType<AppDbContext, AppDbContext>(
                new HierarchicalLifetimeManager());

            // Identity Stores
            container.RegisterType<IUserStore<ApplicationUser, int>,
                ApplicationUserStore>(new HierarchicalLifetimeManager());

            container.RegisterType<IRoleStore<CustomRole, int>,
                ApplicationRoleStore>(new HierarchicalLifetimeManager());

            // Identity Managers
            container.RegisterType<UserManager<ApplicationUser, int>,
                ApplicationUserManager>(new HierarchicalLifetimeManager());

            container.RegisterType<RoleManager<CustomRole, int>,
                ApplicationRoleManager>(new HierarchicalLifetimeManager());

            // Repositorios
            container.RegisterType<IProductoRepository, ProductRepository>();
            container.RegisterType<IResennaRepository, ResennaRepository>();
            container.RegisterType<IOrdenRepository, OrdenRepository>();
            container.RegisterType<IDetalleOrdenRepository, DetalleOrdenRepository>();
            container.RegisterType<IEstadoRepository, EstadoRepository>();
            container.RegisterType<IImagenProductoRepository, ImagenProductoRepository>();

            // Servicios
            container.RegisterType<IProductoService, ProductoService>();
            container.RegisterType<IResennaService, ResennaService>();
            container.RegisterType<IDashboardService, DashboardService>();
            container.RegisterType<IOrdenService, OrdenService>();
            container.RegisterType<IDetalleOrdenService, DetalleOrdenService>();
            container.RegisterType<IEstadoService, EstadoService>();
            container.RegisterType<IImagenProductoService, ImagenProductoService>();
            container.RegisterType<IAuthService, AuthService>();


            // Conectar Unity a Web API
            GlobalConfiguration.Configuration.DependencyResolver =
                new UnityDependencyResolver(container);
        }
    }
}
