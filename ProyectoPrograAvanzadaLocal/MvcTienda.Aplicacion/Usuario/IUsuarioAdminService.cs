using System.Collections.Generic;

namespace MvcTienda.Aplicacion.Usuarios
{
    public interface IUsuarioAdminService
    {
        IEnumerable<UsuarioAdminDto> GetAll();
        UsuarioAdminDto GetById(int id);
        bool CambiarEstado(int id, int nuevoEstado);
    }
}
