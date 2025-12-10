using System.Web.Mvc;

namespace MvcTienda.Web.Controllers
{
    public class ImagenProductoController : Controller
    {
        
        public ActionResult Index()
        {
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
