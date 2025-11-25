using MvcTienda.Aplicacion.Productos;
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
                return View();
            }
        }
        public ActionResult Create()
        {
            return View(new ProductoDto());
        }
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
    }
}