
using System.Web.Mvc;
using MvcTienda.Aplicacion.Dashboard;

namespace MvcTienda.Web.Controllers
{
    [AllowAnonymous] // esto e stemporal para que funcione sin rol 
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetEstadisticas()
        {
            var estadisticas = _dashboardService.ObtenerEstadisticas();
            return Json(estadisticas, JsonRequestBehavior.AllowGet);
        }
    }
}


