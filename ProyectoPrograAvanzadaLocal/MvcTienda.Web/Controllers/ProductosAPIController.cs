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
    [Authorize(Roles = "Administrador")]
    public ActionResult Crear()
    {
        return View();
    }
    [Authorize(Roles = "Administrador")]
    public ActionResult Editar(int id)
    {
        ViewBag.Id = id;
        return View();
    }
    [Authorize(Roles = "Administrador")]
    public ActionResult CambiarEstado(int id)
    {
        ViewBag.Id = id;
        return View();
    }
}

