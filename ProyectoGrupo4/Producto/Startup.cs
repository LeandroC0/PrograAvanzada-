using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Producto.Startup))]
namespace Producto
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
