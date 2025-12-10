using MvcTienda.Aplicacion.Resennas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTienda.Web.Controllers
{
    public class ResennaApiController : Controller
    {
        private readonly IResennaService _service;

        public ResennaApiController(IResennaService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var resennas = _service.GetAllPendiente();
            return View(resennas);
        }
    }
}