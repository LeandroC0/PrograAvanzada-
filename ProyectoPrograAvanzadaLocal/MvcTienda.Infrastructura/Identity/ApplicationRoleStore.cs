using Microsoft.AspNet.Identity.EntityFramework;
using MvcTienda.Infrastructura.Data;

namespace MvcTienda.Infrastructura.Identity
{
    public class ApplicationRoleStore :
        RoleStore<CustomRole, int, CustomUserRole>
    {
        public ApplicationRoleStore(AppDbContext context) : base(context) { }
    }
}
