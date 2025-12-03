using MvcTienda.Aplicacion.Productos;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult Index(string searchTerm = "", int? estadoId = null)
        {
            try
            {
                ViewData["TituloPagina"] = "Catálogo de Productos";
                ViewBag.Mensaje = TempData["Mensaje"];
                ViewBag.SearchTerm = searchTerm;
                ViewBag.EstadoId = estadoId;
                ViewBag.IsAdmin = User.IsInRole("Administrador");

                IEnumerable<ProductoDto> productos;

                if (User.IsInRole("Administrador") && (!string.IsNullOrEmpty(searchTerm) || estadoId.HasValue))
                {
                    productos = _service.Search(searchTerm, estadoId);
                }
                else
                {
                    productos = _service.GetAll();

                    if (!User.IsInRole("Administrador"))
                    {
                        productos = productos.Where(p => p.EstadoId == 1);
                    }
                }

                return View(productos);
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Error al cargar los productos: " + ex.Message;
                return View(new List<ProductoDto>());
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                var producto = _service.GetById(id);
                if (producto == null || (producto.EstadoId != 1 && !User.IsInRole("Administrador")))
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

        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.ListaProductos = new SelectList(_service.GetAll(), "Id", "Nombre");

            var dto = new ProductoDto { EstadoId = 1 }; 
            return View(dto);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductoDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ListaProductos = new SelectList(_service.GetAll(), "Id", "Nombre");
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

        [Authorize(Roles = "Administrador")]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult ChangeStatus(int id, int estadoId)
        {
            try
            {
                _service.ChangeStatus(id, estadoId);

                var mensaje = estadoId == 1
                    ? "Producto activado exitosamente."
                    : "Producto desactivado exitosamente.";

                TempData["Mensaje"] = mensaje;
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Error al cambiar el estado del producto: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Administrador")]
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            return ChangeStatus(id, 2); 
        }
    }
}