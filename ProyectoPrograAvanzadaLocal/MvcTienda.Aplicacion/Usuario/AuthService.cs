using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MvcTienda.Aplicacion.Seguridad;
using MvcTienda.Infrastructura.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTienda.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser, int> _userManager;
        private readonly SignInManager<ApplicationUser, int> _signInManager;

        public AuthService(
    UserManager<ApplicationUser, int> userManager,
    SignInManager<ApplicationUser, int> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public string GenerarCodigoUsuario(string usuario)
        {
            string letras = usuario.Length >= 3 ? usuario.Substring(0, 3).ToUpper() : usuario.ToUpper();
            var random = new Random();
            int numeros = random.Next(0, 9999);
            string formatoNumeros = numeros.ToString("D4");
            return $"{letras}-{formatoNumeros}";
        }
        public async Task<bool> Register(string usuario, string password)
        {

            var user = new ApplicationUser
            {
                UserName = usuario,
                Email = usuario + "@correo.com",
                EstadoId = 1,
                CodigoUsuario = GenerarCodigoUsuario(usuario),
                FechaUltimaConexion = null
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return false;

            await _userManager.AddToRoleAsync(user.Id, "Asociado");

            return true;
        }

        public async Task<bool> Login(string usuario, string password)
        {
            var user = await _userManager.FindByNameAsync(usuario);

            if (user == null)
                return false;

            // Valida estado del usuario
            if (user.EstadoId != 1)
                return false;
             
            var result = await _signInManager.PasswordSignInAsync(usuario, password, true, false);

            if (result == SignInStatus.Success)
            {
                user.FechaUltimaConexion = DateTime.Now;
                await _userManager.UpdateAsync(user);
                return true;
            }

            return false;
        }


        public async Task Logout()
        {
            _signInManager.AuthenticationManager.SignOut();
        }

        public int ObtenerTotalUsuarios()
        {
            return _userManager.Users.Count();
        }

        public int ObtenerUsuariosActivos()
        {
            return _userManager.Users.Count(u => u.EstadoId == 1);
        }

        public int ObtenerUsuariosInactivos()
        {
            return _userManager.Users.Count(u => u.EstadoId != 1);
        }

        public Task<bool> Register(string usuario, string password, string rol)
        {
            throw new NotImplementedException();
        }
        public async Task<ApplicationUser> BuscarUsuarioAsync(string usuario)
        {
            return await _userManager.FindByNameAsync(usuario);
        }


        public async Task<IdentityResult> RegisterConResultado(string usuario, string password)
        {
            var user = new ApplicationUser
            {
                UserName = usuario,
                Email = usuario + "@correo.com",
                EstadoId = 1,
                CodigoUsuario = GenerarCodigoUsuario(usuario),
                FechaUltimaConexion = null
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user.Id, "Asociado");

            return result;
        }

    }
}
