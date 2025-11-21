using Microsoft.AspNet.Identity.EntityFramework;
using MvcTienda.Infrastructura.Data;
using MvcTienda.Infrastrutura.Data;

namespace MvcTienda.Infrastrutura.Identity
{
    public class ApplicationRoleStore :
        RoleStore<CustomRole, int, CustomUserRole>
    {
        public ApplicationRoleStore(AppDbContext context) : base(context) { }
    }
}
