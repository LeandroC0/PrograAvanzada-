using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MvcTienda.Aplicacion.Dashboard;
using MvcTienda.Aplicacion.DetallesOrden;
using MvcTienda.Aplicacion.Estados;
using MvcTienda.Aplicacion.Imagenes;
using MvcTienda.Aplicacion.Ordenes;
using MvcTienda.Aplicacion.Productos;
using MvcTienda.Aplicacion.Resennas;
using MvcTienda.Aplicacion.Seguridad;
using MvcTienda.Aplicacion.Usuarios;
using MvcTienda.Auth.Services;
using MvcTienda.Domain.Repositories;
using MvcTienda.Infrastructura.Data;
using MvcTienda.Infrastructura.Identity;
using MvcTienda.Infrastructura.Repositories;
using System;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;

namespace MvcTienda.Web
{
    public static class UnityConfig
    {
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        public static IUnityContainer Container => container.Value;

        public static void RegisterComponents()
        {
            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            // DbContext
            container.RegisterType<AppDbContext>(new PerRequestLifetimeManager());

            // Repositorios
            container.RegisterType<IEstadoRepository, EstadoRepository>();
            container.RegisterType<IProductoRepository, ProductRepository>();
            container.RegisterType<IOrdenRepository, OrdenRepository>();
            container.RegisterType<IDetalleOrdenRepository, DetalleOrdenRepository>();
            container.RegisterType<IResennaRepository, ResennaRepository>();
             container.RegisterType<IOrdenRepository, OrdenRepository>();
            container.RegisterType<IImagenProductoRepository, ImagenProductoRepository>();

            // Servicios de aplicación
            container.RegisterType<IEstadoService, EstadoService>();
            container.RegisterType<IProductoService, ProductoService>();
            container.RegisterType<IImagenProductoService, ImagenProductoService>();
            container.RegisterType<IDashboardService, DashboardService>();
            container.RegisterType<IResennaService, ResennaService>();
            container.RegisterType<IOrdenService, OrdenService>();
            container.RegisterType<IAuthService, AuthService>(); 
            container.RegisterType<IOrdenService, OrdenService>();
            container.RegisterType<IDetalleOrdenService, DetalleOrdenService>();
            container.RegisterType<IUsuarioAdminService, UsuarioAdminService>();



            // Identity stores
            container.RegisterType<IUserStore<ApplicationUser, int>, ApplicationUserStore>(
                new PerRequestLifetimeManager());
            container.RegisterType<IRoleStore<CustomRole, int>, ApplicationRoleStore>(
                new PerRequestLifetimeManager());

            // Managers
            container.RegisterType<ApplicationUserManager>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationSignInManager>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationRoleManager>(new PerRequestLifetimeManager());

            //prueba dashboard
            container.RegisterType<IAuthenticationManager>(
                new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));

            container.RegisterType<UserManager<ApplicationUser, int>>(
                new PerRequestLifetimeManager(),
                new InjectionFactory(o => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()));

            container.RegisterType<SignInManager<ApplicationUser, int>>(
                new PerRequestLifetimeManager(),
                new InjectionFactory(o => HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>()));

          

          
        }
    }
}

