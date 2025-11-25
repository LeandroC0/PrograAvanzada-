using MvcTienda.Aplicacion.Productos;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcTienda.Web.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoService _service;

        public ProductoController(IProductoService service)
        {
            _service = service;
        }

        // GET: Producto
        public ActionResult Index()
        {
            try
            {
                ViewData["TituloPagina"] = "Lista de productos";
                ViewBag.Mensaje = TempData["Mensaje"];
                var productos = _service.GetAll();
                return View(productos);
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Error al cargar los productos: " + ex.Message;
                return View(new List<ProductoDto>());
            }
        }

        // GET: Producto/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var producto = _service.GetById(id);
                if (producto == null)
                {
                    return HttpNotFound();
                }
                return View(producto);
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Error al obtener el producto: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            return View(new ProductoDto());
        }

        // POST: Producto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                _service.Create(dto);
                TempData["Mensaje"] = "Producto creado exitosamente.";
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Error al crear el producto: " + ex.Message;
                return View(dto);
            }
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var producto = _service.GetById(id);
                if (producto == null)
                {
                    return HttpNotFound();
                }
                return View(producto);
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Error al obtener el producto: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // POST: Producto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                _service.Update(dto);
                TempData["Mensaje"] = "Producto actualizado exitosamente.";
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Error al actualizar el producto: " + ex.Message;
                return View(dto);
            }
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var producto = _service.GetById(id);
                if (producto == null)
                {
                    return HttpNotFound();
                }
                return View(producto);
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Error al obtener el producto: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _service.Delete(id);
                TempData["Mensaje"] = "Producto eliminado exitosamente.";
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Error al eliminar el producto: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
