using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTienda.Web.Controllers
{
    public class DashboardAPIController : Controller
    {
        // GET: DashboardAPI
        public ActionResult Index()
        {
            return View();
        }
    }
}