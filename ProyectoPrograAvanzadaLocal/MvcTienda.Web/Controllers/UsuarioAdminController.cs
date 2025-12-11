using MvcTienda.Aplicacion.Usuarios;
using System.Web.Mvc;

namespace MvcTienda.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuarioAdminController : Controller
    {
        private readonly IUsuarioAdminService _service;

        public UsuarioAdminController(IUsuarioAdminService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var lista = _service.GetAll();
            return View(lista);
        }

        public ActionResult Edit(int id)
        {
            var user = _service.GetById(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(int usuarioId, int estadoId)
        {
            _service.CambiarEstado(usuarioId, estadoId);
            return RedirectToAction("Index");
        }

        public ActionResult Activar(int id)
        {
            _service.CambiarEstado(id, 1);
            return RedirectToAction("Index");
        }

        public ActionResult Desactivar(int id)
        {
            _service.CambiarEstado(id, 2);
            return RedirectToAction("Index");
        }
    }
}
