using MvcTienda.Aplicacion.Productos;
using MvcTienda.Aplicacion.Resennas;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace MvcTienda.Web.Controllers
{
    public class ImagenProductoController : Controller
    {
        private readonly IProductoService _productoService;

        public ImagenProductoController( IProductoService productoService)
        {
            _productoService = productoService;
        }

        public ActionResult Index()
        {
            ViewBag.ListaProductos = new SelectList(_productoService.GetAll(), "ProductoId", "Nombre");

            return View();
        }

        
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        
        [Authorize(Roles = "Administrador")]
        public ActionResult Details(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}
