using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace MvcTienda.Infrastrutura.Identity
{
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public DateTime? FechaUltimaConexion { get; set; }
        public int Estado { get; set; }

        // En Identity usaremos UserName como "NombreUsuario"
        // y PasswordHash como "Contrasena"
    }

    public class CustomUserLogin : IdentityUserLogin<int> { }
    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
}
