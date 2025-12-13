using MvcTienda.Aplicacion.Resennas;
using System.Collections.Generic;
using System.Web.Mvc;

public class ProductosAPIController : Controller
{

    private readonly IResennaService _resennaService;
    public ActionResult Index()
    {
        return View();
    }

    public ActionResult Detalle(int id)
    {
        ViewBag.Id = id;
        IEnumerable<ResennaDto> resennas = _resennaService.GetAllByProductoId(id);
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

