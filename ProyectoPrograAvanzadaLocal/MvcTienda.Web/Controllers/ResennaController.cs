using MvcTienda.Aplicacion.Resennas;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcTienda.Web.Controllers
{
    public class ResennaController : Controller
    {
        private readonly IResennaService _service;

        public ResennaController(IResennaService service)
        {
            _service = service;
        }

        // GET: Resenna
        public ActionResult Index()
        {
            try
            {
                ViewData["TituloPagina"] = "Lista de reseñas";
                ViewBag.Mensaje = TempData["Mensaje"];
                var resennas = _service.GetAll();
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
            return View(new ResennaDto());
        }

        // POST: Resenna/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ResennaDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            try
            {
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
            if (!ModelState.IsValid) return View(dto);

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
