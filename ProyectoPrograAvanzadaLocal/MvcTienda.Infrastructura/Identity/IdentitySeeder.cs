using Microsoft.AspNet.Identity;
using MvcTienda.Infrastructura.Data;
using System.Linq;

namespace MvcTienda.Infrastrutura.Identity
{
    public static class IdentitySeeder
    {
        public static void Seed()
        {
            using (var context = new AppDbContext())
            {
                var roleManager = new ApplicationRoleManager(
                    new ApplicationRoleStore(context));

                var userManager = new ApplicationUserManager(
                    new ApplicationUserStore(context));

                // 1. Crear roles si no existen
                string[] roles = { "Administrador", "Cliente" };

                foreach (var rol in roles)
                {
                    if (!roleManager.RoleExists(rol))
                    {
                        roleManager.Create(new CustomRole { Name = rol });
                    }
                }

                // 2. Crear primer usuario administrador solo si no existe
                if (!context.Users.Any())
                {
                    var admin = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = "admin@tienda.com",
                        Estado = 1
                    };

                    var result = userManager.Create(admin, "Admin123!");

                    if (result.Succeeded)
                    {
                        userManager.AddToRole(admin.Id, "Administrador");
                    }
                }

                context.SaveChanges();
            }
        }
    }
}
