using MvcTienda.Aplicacion.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MvcTienda.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string usuario, string password)
        {
            var ok = await _authService.Login(usuario, password);

            if (!ok)
            {
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(string usuario, string password)
        {
            // Validar usuario vacío
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Debe ingresar un usuario y una contraseña.";
                return View();
            }

            //  Validar si el usuario ya existe
            var existente = await _authService.BuscarUsuarioAsync(usuario);
            if (existente != null)
            {
                ViewBag.Error = $"El usuario '{usuario}' ya existe. Intente otro nombre.";
                return View();
            }

            // 2️ Intentar registrar y capturar errores de contraseña
            var resultado = await _authService.RegisterConResultado(usuario, password);

            if (!resultado.Succeeded)
            {
                ViewBag.Error = string.Join("<br/>", resultado.Errors);
                return View();
            }


            return RedirectToAction("Login");
        }

        // Logout
        public async Task<ActionResult> Logout()
        {
            await _authService.Logout();
            return RedirectToAction("Login");
        }
    }
}