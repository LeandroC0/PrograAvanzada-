using Microsoft.AspNet.Identity.EntityFramework;
using MvcTienda.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MvcTienda.Infrastructura.Identity
{
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public string CodigoUsuario { get; set; }
        public DateTime? FechaUltimaConexion { get; set; }
        public int EstadoId { get; set; }
        public virtual Estado Estado { get; set; }


        public virtual ICollection<Orden> Ordenes { get; set; }

        public static string ReferenceEquals(int usuarioId)
        {
            throw new NotImplementedException();
        }
    }

    public class CustomUserLogin : IdentityUserLogin<int> { }
    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
}
