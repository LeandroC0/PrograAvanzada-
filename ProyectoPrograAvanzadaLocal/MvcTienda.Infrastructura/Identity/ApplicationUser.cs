using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace MvcTienda.Infrastructura.Identity
{
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public DateTime? FechaUltimaConexion { get; set; }
        public int Estado { get; set; }

    }

    public class CustomUserLogin : IdentityUserLogin<int> { }
    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
}
