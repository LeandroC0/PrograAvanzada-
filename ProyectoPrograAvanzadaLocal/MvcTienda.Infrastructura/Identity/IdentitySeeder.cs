using Microsoft.AspNet.Identity;
using MvcTienda.Infrastructura.Data;
using System.Linq;

namespace MvcTienda.Infrastructura.Identity
{
    public static class IdentitySeeder
    {
        public static void Seed(AppDbContext context,
                                ApplicationUserManager userManager,
                                ApplicationRoleManager roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new CustomRole("Administrador"));
                roleManager.Create(new CustomRole("Asociado"));
            }
            if (!userManager.Users.Any(u => u.UserName == "admin"))
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@proyecto.com",
                    Estado = 1
                };
                userManager.Create(admin, "Admin123!");
                userManager.AddToRole(admin.Id, "Administrador");
            }
        }
    }
}
