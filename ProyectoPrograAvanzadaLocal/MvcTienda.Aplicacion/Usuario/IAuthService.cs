using System.Threading.Tasks;

namespace MvcTienda.Aplicacion.Seguridad
{
    public interface IAuthService
    {
        Task<bool> Register(string usuario, string password, string rol);
        Task<bool> Login(string usuario, string password);
        Task Logout();


        int ObtenerTotalUsuarios();
        int ObtenerUsuariosActivos();
        int ObtenerUsuariosInactivos();
    }
}
