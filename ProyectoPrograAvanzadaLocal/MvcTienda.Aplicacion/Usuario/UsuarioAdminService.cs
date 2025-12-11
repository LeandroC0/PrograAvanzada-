using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using MvcTienda.Infrastructura.Identity;

namespace MvcTienda.Aplicacion.Usuarios
{
    public class UsuarioAdminService : IUsuarioAdminService
    {
        private readonly UserManager<ApplicationUser, int> _userManager;

        public UsuarioAdminService(UserManager<ApplicationUser, int> userManager)
        {
            _userManager = userManager;
        }

        public IEnumerable<UsuarioAdminDto> GetAll()
        {
            return _userManager.Users.Select(u => new UsuarioAdminDto
            {
                UsuarioId = u.Id,
                Usuario = u.UserName,
                Correo = u.Email,
                EstadoId = u.EstadoId,
                EstadoNombre = u.EstadoId == 1 ? "Activo" : "Inactivo"
            }).ToList();
        }

        public UsuarioAdminDto GetById(int id)
        {
            var u = _userManager.FindById(id);
            if (u == null) return null;

            return new UsuarioAdminDto
            {
                UsuarioId = u.Id,
                Usuario = u.UserName,
                Correo = u.Email,
                EstadoId = u.EstadoId,
                EstadoNombre = u.EstadoId == 1 ? "Activo" : "Inactivo"
            };
        }

        public bool CambiarEstado(int id, int nuevoEstado)
        {
            var u = _userManager.FindById(id);
            if (u == null) return false;

            u.EstadoId = nuevoEstado;

            var result = _userManager.Update(u);
            return result.Succeeded;
        }
    }
}
