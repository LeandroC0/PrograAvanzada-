using Microsoft.AspNet.Identity;
using MvcTienda.Aplicacion.DetallesOrden;
using MvcTienda.Aplicacion.Estados;
using MvcTienda.Aplicacion.Imagenes;
using MvcTienda.Aplicacion.Ordenes;
using MvcTienda.Aplicacion.Productos;
using MvcTienda.Aplicacion.Resennas;
using MvcTienda.Domain.Repositories;
using MvcTienda.Infrastructura.Data;
using MvcTienda.Infrastructura.Repositories;
using MvcTienda.Infrastrutura.Identity;
using System;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace MvcTienda.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
        }

        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // DbContext por request
            container.RegisterType<AppDbContext, AppDbContext>(new PerRequestLifetimeManager());

            // Repositorios
            container.RegisterType<IProductoRepository, ProductRepository>();
            container.RegisterType<IEstadoRepository, EstadoRepository>();
            container.RegisterType<IImagenProductoRepository, ImagenProductoRepository>();
            container.RegisterType<IOrdenRepository, OrdenRepository>();
            container.RegisterType<IDetalleOrdenRepository, DetalleOrdenRepository>();
            container.RegisterType<IResennaRepository, ResennaRepository>();

            // Servicios
            container.RegisterType<IProductoService, ProductoService>();
            container.RegisterType<IEstadoService, EstadoService>();
            container.RegisterType<IImagenProductoService, ImagenProductoService>();
            container.RegisterType<IOrdenService, OrdenService>();
            container.RegisterType<IDetalleOrdenService, DetalleOrdenService>();
            container.RegisterType<IResennaService, ResennaService>();

            // Identity stores
            container.RegisterType<IUserStore<ApplicationUser, int>, ApplicationUserStore>(new PerRequestLifetimeManager());
            container.RegisterType<IRoleStore<CustomRole, int>, ApplicationRoleStore>(new PerRequestLifetimeManager());

            // Managers (por request)
            container.RegisterType<ApplicationUserManager>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationSignInManager>(new PerRequestLifetimeManager());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }


    }
}