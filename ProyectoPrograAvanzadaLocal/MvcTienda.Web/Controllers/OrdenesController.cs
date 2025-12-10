using Microsoft.AspNet.Identity;
using MvcTienda.Aplicacion.Ordenes;
using System.Linq;
using System.Web.Mvc;

namespace MvcTienda.Web.Controllers
{
    public class OrdenesController : Controller
    {
        private readonly IOrdenService _service;

        public OrdenesController(IOrdenService service)
        {
            _service = service;
        }

        // GET: Orden
        public ActionResult Index()
        {
            var lista = _service.GetAll();
            return View(lista);
        }

        // GET: Orden/Details/5
        public ActionResult Details(int id)
        {
            var orden = _service.GetById(id);
            if (orden == null) return HttpNotFound();
            return View(orden);
        }

        // GET: Orden/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orden/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrdenDto dto)
        {
            if (ModelState.IsValid)
            {
                _service.Create(dto);
                return RedirectToAction("Index");
            }
            return View(dto);
        }

        // GET: Orden/Edit/5
        public ActionResult Edit(int id)
        {
            var dto = _service.GetById(id);
            if (dto == null) return HttpNotFound();

            return View(dto);
        }

        // POST: Orden/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrdenDto dto)
        {
            if (ModelState.IsValid)
            {
                _service.Update(dto);
                return RedirectToAction("Index");
            }

            return View(dto);
        }

        // GET: Orden/Delete/5
        public ActionResult Delete(int id)
        {
            var dto = _service.GetById(id);
            if (dto == null) return HttpNotFound();

            return View(dto);
        }

        // POST: Orden/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult MisOrdenes()
        {
            int usuarioId = User.Identity.GetUserId<int>();

            var ordenes = _service
                .GetAll()
                .Where(o => o.UsuarioId == usuarioId)
                .OrderByDescending(o => o.Fecha_Orden)
                .ToList();

            return View(ordenes);
        }

    }
}
