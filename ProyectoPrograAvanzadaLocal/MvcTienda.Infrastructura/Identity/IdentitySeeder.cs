using Microsoft.AspNet.Identity;
using MvcTienda.Infrastructura.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTienda.Infrastructura.Identity
{
    public static class IdentitySeeder
    {
        public static async Task Seed(
            RoleManager<CustomRole, int> roleManager,
            UserManager<ApplicationUser, int> userManager)
        {
            if (!await roleManager.RoleExistsAsync("Administrador"))
                await roleManager.CreateAsync(new CustomRole { Name = "Administrador" });

            if (!await roleManager.RoleExistsAsync("Asociado"))
                await roleManager.CreateAsync(new CustomRole { Name = "Asociado" });

            var admin = await userManager.FindByNameAsync("admin");
            if (admin == null)
            {
                var newAdmin = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@tienda.com",
                    EstadoId = 1
                };

                var result = await userManager.CreateAsync(newAdmin, "Admin123*");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(newAdmin.Id, "Administrador");
            }
        }
    }
}
