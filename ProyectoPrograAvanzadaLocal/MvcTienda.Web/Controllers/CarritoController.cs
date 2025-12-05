using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTienda.Web.Controllers
{
    [Authorize(Roles = "Asociado")]
    public class CarritoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
