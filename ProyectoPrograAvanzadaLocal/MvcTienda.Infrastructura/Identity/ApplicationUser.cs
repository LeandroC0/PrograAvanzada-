using Microsoft.AspNet.Identity.EntityFramework;
using MvcTienda.Domain.Entities;
using System;

namespace MvcTienda.Infrastructura.Identity
{
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public string CodigoUsuario { get; set; }
        public DateTime? FechaUltimaConexion { get; set; }
        public int EstadoId { get; set; }
        public virtual Estado Estado { get; set; }

    }

    public class CustomUserLogin : IdentityUserLogin<int> { }
    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
}
