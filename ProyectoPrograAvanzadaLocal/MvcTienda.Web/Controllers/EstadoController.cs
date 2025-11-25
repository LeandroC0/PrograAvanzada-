using MvcTienda.Aplicacion.Estados;
using System.Web.Mvc;

namespace MvcTienda.Web.Controllers
{
    public class EstadoController : Controller
    {
        private readonly IEstadoService _service;
        public EstadoController(IEstadoService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            try
            {
                ViewData["TituloPagina"] = "Lista de estados";
                ViewBag.Mensaje = TempData["Mensaje"];
                var estados = _service.GetAll();
                return View(estados);
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Error al cargar los estados: " + ex.Message;
                return View(new EstadoDto());
            }
        }

        public ActionResult Create()
        {
            return View(new EstadoDto());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EstadoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                _service.Create(dto);
                TempData["Mensaje"] = "Estado creado exitosamente.";
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Error al crear el estado: " + ex.Message;
                return View(dto);
            }
        }
    }
}