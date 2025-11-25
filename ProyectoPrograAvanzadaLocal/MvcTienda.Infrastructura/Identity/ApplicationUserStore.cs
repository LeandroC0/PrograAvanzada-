using Microsoft.AspNet.Identity.EntityFramework;
using MvcTienda.Infrastructura.Data;


namespace MvcTienda.Infrastructura.Identity
{
    public class ApplicationUserStore :
        UserStore<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationUserStore(AppDbContext context)
            : base(context)
        {
        }
    }
}
