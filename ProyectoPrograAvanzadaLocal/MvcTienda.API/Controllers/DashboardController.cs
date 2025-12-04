using MvcTienda.Aplicacion.Dashboard;
using System;
using System.Web.Http;

[RoutePrefix("api/dashboard")]
[Authorize(Roles = "Administrador")]
public class DashboardController : ApiController
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    // GET api/dashboard/estadisticas
    [HttpGet]
    [Route("estadisticas")]
    public IHttpActionResult GetEstadisticas()
    {
        try
        {
            var estadisticas = _dashboardService.ObtenerEstadisticas();
            return Ok(estadisticas);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }

    // GET api/dashboard/estadisticas/detalladas
    [HttpGet]
    [Route("estadisticas/detalladas")]
    public IHttpActionResult GetEstadisticasDetalladas()
    {
        try
        {
            var estadisticas = _dashboardService.ObtenerEstadisticas();
            return Ok(estadisticas);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }
}
