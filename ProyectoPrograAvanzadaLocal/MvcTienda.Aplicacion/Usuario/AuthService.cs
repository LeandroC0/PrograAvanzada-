using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MvcTienda.Aplicacion.Seguridad;
using MvcTienda.Infrastructura.Identity;
using System.Threading.Tasks;

namespace MvcTienda.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser, int> _userManager;
        private readonly SignInManager<ApplicationUser, int> _signInManager;

        public AuthService(UserManager<ApplicationUser, int> userManager, SignInManager<ApplicationUser, int> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Register(string usuario, string password, string rol)
        {
            var user = new ApplicationUser
            {
                UserName = usuario,
                Email = usuario + "@correo.com",
                Estado = 1
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return false;

            await _userManager.AddToRoleAsync(user.Id, rol);

            return true;
        }

        public async Task<bool> Login(string usuario, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(usuario, password, false, false);

            return result == SignInStatus.Success;
        }
        
        public async Task Logout()
        {
            _signInManager.AuthenticationManager.SignOut();
        }
    }
}
