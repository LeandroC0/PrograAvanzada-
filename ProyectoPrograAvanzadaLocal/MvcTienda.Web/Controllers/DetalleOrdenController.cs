using MvcTienda.Aplicacion.DetallesOrden;
using MvcTienda.Aplicacion.Productos;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcTienda.Web.Controllers
{
    public class DetalleOrdenController : Controller
    {
        private readonly IDetalleOrdenService _service;
        private readonly IProductoService _productoService;

        public DetalleOrdenController(IDetalleOrdenService service, IProductoService productoService)
        {
            _service = service;
                _productoService = productoService;
        }

        // GET: DetalleOrden
        public ActionResult Index()
        {
            try
            {
                ViewData["TituloPagina"] = "Lista de Detalles de Orden";
                ViewBag.Mensaje = TempData["Mensaje"];

                var detalles = _service.GetAll();
                return View(detalles);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al cargar los detalles de orden: " + ex.Message;
                return View(new List<DetalleOrdenDto>());
            }
        }

        // GET: DetalleOrden/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var detalle = _service.GetById(id);
                if (detalle == null)
                {
                    return HttpNotFound();
                }
                return View(detalle);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al obtener el detalle: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: DetalleOrden/Create
        public ActionResult Create()
        {
            ViewBag.ListaProductos = new SelectList(_productoService.GetAll(), "ProductoId", "Nombre");

            return View(new DetalleOrdenDto());
        }

        // POST: DetalleOrden/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DetalleOrdenDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ListaProductos = new SelectList(_productoService.GetAll(), "ProductoId", "Nombre");

                return View(dto);
            }

            try
            {
                _service.Create(dto);
                TempData["Mensaje"] = "Detalle de orden creado exitosamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al crear el detalle: " + ex.Message;
                return View(dto);
            }
        }

        // GET: DetalleOrden/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var detalle = _service.GetById(id);
                if (detalle == null)
                {
                    return HttpNotFound();
                }

                return View(detalle);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al obtener el detalle para edición: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // POST: DetalleOrden/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DetalleOrdenDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                _service.Update(dto);
                TempData["Mensaje"] = "Detalle de orden actualizado exitosamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al actualizar el detalle: " + ex.Message;
                return View(dto);
            }
        }

        // GET: DetalleOrden/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var detalle = _service.GetById(id);
                if (detalle == null)
                {
                    return HttpNotFound();
                }

                return View(detalle);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al obtener el detalle: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // POST: DetalleOrden/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _service.Delete(id);
                TempData["Mensaje"] = "Detalle de orden eliminado exitosamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al eliminar el detalle: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
