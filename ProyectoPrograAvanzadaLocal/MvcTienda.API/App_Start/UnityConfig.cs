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

            // Servicios
            container.RegisterType<IProductoService, ProductoService>();
            container.RegisterType<IResennaService, ResennaService>();

            // Conectar Unity a Web API
            GlobalConfiguration.Configuration.DependencyResolver =
                new UnityDependencyResolver(container);
        }
    }
}
