using MvcTienda.Aplicacion.Estados;
using System;
using System.Collections.Generic;
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

        // GET: Estado
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
                return View(new List<EstadoDto>());
            }
        }

        // GET: Estado/Details/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Details(int id)
        {
            try
            {
                var estado = _service.GetById(id);
                if (estado == null)
                {
                    return HttpNotFound();
                }
                return View(estado);
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Error al obtener el estado: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: Estado/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View(new EstadoDto());
        }

        // POST: Estado/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
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

        // GET: Estado/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int id)
        {
            try
            {
                var estado = _service.GetById(id);
                if (estado == null)
                {
                    return HttpNotFound();
                }
                return View(estado);
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Error al obtener el estado: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // POST: Estado/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(EstadoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                _service.Update(dto);
                TempData["Mensaje"] = "Estado actualizado exitosamente.";
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Error al actualizar el estado: " + ex.Message;
                return View(dto);
            }
        }

        // GET: Estado/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int id)
        {
            try
            {
                var estado = _service.GetById(id); 
                if (estado == null)
                {
                    return HttpNotFound();
                }
                return View(estado);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al obtener el estado: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // POST: Estado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _service.Delete(id);
                TempData["Mensaje"] = "Estado eliminado exitosamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al eliminar el estado: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
