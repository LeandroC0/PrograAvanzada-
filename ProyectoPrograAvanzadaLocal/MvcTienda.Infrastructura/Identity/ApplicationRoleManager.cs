using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace MvcTienda.Infrastrutura.Identity
{
    public class ApplicationRoleManager : RoleManager<CustomRole, int>
    {
        public ApplicationRoleManager(IRoleStore<CustomRole, int> roleStore)
            : base(roleStore)
        {
        }

        
        public static ApplicationRoleManager Create(
            Microsoft.Owin.IOwinContext context)
        {
            var roleStore = new ApplicationRoleStore(context.Get<MvcTienda.Infrastructura.Data.AppDbContext>());
            return new ApplicationRoleManager(roleStore);
        }
    }
}
