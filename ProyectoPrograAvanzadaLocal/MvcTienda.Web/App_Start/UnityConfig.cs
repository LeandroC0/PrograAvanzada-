using Microsoft.AspNet.Identity;
using MvcTienda.Aplicacion.Estados;
using MvcTienda.Aplicacion.Imagenes;
using MvcTienda.Aplicacion.Productos;
using MvcTienda.Domain.Repositories;
using MvcTienda.Infrastructura.Data;
using MvcTienda.Infrastructura.Identity;
using MvcTienda.Infrastructura.Repositories;
using System;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace MvcTienda.Web
{
    public static class UnityConfig
    {
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var c = new UnityContainer();
              RegisterTypes(c);
              return c;
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
            container.RegisterType<IImagenProductoRepository, ImagenProductoRepository>();

            // Servicios de aplicación
            container.RegisterType<IEstadoService, EstadoService>();
            container.RegisterType<IProductoService, ProductoService>();
            container.RegisterType<IImagenProductoService, ImagenProductoService>();
            // y así con OrdenService, DetalleOrdenService, ResennaService, etc.

            // Identity stores
            container.RegisterType<IUserStore<ApplicationUser, int>, ApplicationUserStore>(
                new PerRequestLifetimeManager());
            container.RegisterType<IRoleStore<CustomRole, int>, ApplicationRoleStore>(
                new PerRequestLifetimeManager());

            // Managers
            container.RegisterType<ApplicationUserManager>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationSignInManager>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationRoleManager>(new PerRequestLifetimeManager());
        }
    }
}
