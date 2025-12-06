using System.Web.Mvc;

public class ProductosAPIController : Controller
{
    public ActionResult Index()
    {
        return View();
    }

    public ActionResult Detalle(int id)
    {
        ViewBag.Id = id;
        return View();
    }

    public ActionResult Crear()
    {
        return View();
    }

    public ActionResult Editar(int id)
    {
        ViewBag.Id = id;
        return View();
    }
    public ActionResult CambiarEstado(int id)
    {
        ViewBag.Id = id;
        return View();
    }
}

