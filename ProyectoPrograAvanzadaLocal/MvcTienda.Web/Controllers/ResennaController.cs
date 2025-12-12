using Microsoft.AspNet.Identity;
using MvcTienda.Aplicacion.Productos;
using MvcTienda.Aplicacion.Resennas;
using MvcTienda.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcTienda.Web.Controllers
{
    public class ResennaController : Controller
    {
        private readonly IResennaService _service;
        private readonly IProductoService _productoService;
        public ResennaController(IResennaService service, IProductoService productoService)
        {
            _service = service;
            _productoService = productoService;
        }

        // GET: Resenna
        public ActionResult Index(int productoId = 0)
        {
            try
            {
                ViewData["TituloPagina"] = "Lista de reseñas";
                ViewBag.Mensaje = TempData["Mensaje"];
                ViewBag.Productos = _productoService.GetAll();
                IEnumerable<ResennaDto> resennas;

                if (productoId == 0)
                {
                    resennas = _service.GetAllPublic();
                }
                else
                {
                    resennas = _service.GetAllByProductoId(productoId);
                }
                return View(resennas);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al cargar las reseñas: " + ex.Message;
                return View(new List<ResennaDto>());
            }
        }

        public ActionResult MisResennas()
        {
            int usuarioId = User.Identity.GetUserId<int>();
            try
            {
                ViewData["TituloPagina"] = "Mis reseñas";
                var resennas = _service.GetAllByUsuarioId(usuarioId);

                return View(resennas);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al cargar las reseñas: " + ex.Message;
                return View(new List<ResennaDto>());
            }
        }

        // GET: Resenna/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var resenna = _service.GetById(id);
                if (resenna == null) return HttpNotFound();
                return View(resenna);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al obtener la reseña: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: Resenna/Create
        public ActionResult Create()
        {
            int usuarioId = User.Identity.GetUserId<int>();
            string usuarioNombre = User.Identity.GetUserName();

            ViewBag.UsuarioId = usuarioId;
            ViewBag.UsuarioNombre = usuarioNombre;
            ViewBag.Fecha_Resenna = DateTime.Now;

            ViewBag.ListaProductos = new SelectList(_productoService.GetAll(), "ProductoId", "Nombre");
            return View(new ResennaDto());
        }

        // POST: Resenna/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ResennaDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ListaProductos = new SelectList(_productoService.GetAll(), "ProductoId", "Nombre");
                return View(dto);
            }

            try
            {
                dto.UsuarioId = User.Identity.GetUserId<int>();
                _service.Create(dto);
                TempData["Mensaje"] = "Reseña creada exitosamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al crear la reseña: " + ex.Message;
                return View(dto);
            }
        }

        // GET: Resenna/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.ListaProductos = new SelectList(_productoService.GetAll(), "ProductoId", "Nombre");
                var resenna = _service.GetById(id);
                if (resenna == null) return HttpNotFound();
                return View(resenna);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al obtener la reseña: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // POST: Resenna/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ResennaDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ListaProductos = new SelectList(_productoService.GetAll(), "ProductoId", "Nombre");
                return View(dto);
            }
            try
            {
                _service.Update(dto);
                TempData["Mensaje"] = "Reseña actualizada exitosamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al actualizar la reseña: " + ex.Message;
                return View(dto);
            }
        }

        // GET: Resenna/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var resenna = _service.GetById(id);
                if (resenna == null) return HttpNotFound();
                return View(resenna);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al obtener la reseña: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // POST: Resenna/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _service.Delete(id);
                TempData["Mensaje"] = "Reseña eliminada exitosamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al eliminar la reseña: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
