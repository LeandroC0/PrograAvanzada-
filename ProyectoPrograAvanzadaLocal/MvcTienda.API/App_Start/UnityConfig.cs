using MvcTienda.Aplicacion.Dashboard;
using MvcTienda.Aplicacion.DetallesOrden;
using MvcTienda.Aplicacion.Estados;
using MvcTienda.Aplicacion.Imagenes;
using MvcTienda.Aplicacion.Ordenes;
using MvcTienda.Aplicacion.Productos;
using MvcTienda.Aplicacion.Resennas;
using MvcTienda.Domain.Repositories;
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
            container.RegisterType<AppDbContext, AppDbContext>(new HierarchicalLifetimeManager());

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

            // Conectar Unity a Web API
            GlobalConfiguration.Configuration.DependencyResolver =
                new UnityDependencyResolver(container);
        }
    }
}
