using Microsoft.AspNet.Identity;
using MvcTienda.Infrastructura.Identity;
using System.Threading.Tasks;

namespace MvcTienda.Aplicacion.Seguridad
{
    public interface IAuthService
    {
        Task<bool> Register(string usuario, string password);
        Task<bool> Login(string usuario, string password);
        Task Logout();

        Task<ApplicationUser> BuscarUsuarioAsync(string usuario);
        Task<IdentityResult> RegisterConResultado(string usuario, string password);

        int ObtenerTotalUsuarios();
        int ObtenerUsuariosActivos();
        int ObtenerUsuariosInactivos();
    }
}
