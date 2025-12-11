using MvcTienda.Infrastructura.Data;
using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity; 

namespace MvcTienda.API.Controllers
{
    [RoutePrefix("api/dashboard")]
    public class DashboardController : ApiController
    {
        [HttpGet]
        [Route("estadisticas")]
        public IHttpActionResult Estadisticas()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var totalUsuarios = db.Users.Count();

                    var usuariosActivos = db.Users.Count(u => u.EstadoId == 1);
                    var usuariosInactivos = db.Users.Count(u => u.EstadoId != 1);

                    var totalProductos = db.Productos.Count();

                    var productosBajoInventario = db.Productos
                        .Count(p => p.Inventario < 5);

                    var hace7Dias = DateTime.Now.AddDays(-7);

                    var ventasUltimaSemana = db.Ordenes
                        .Where(o => o.Fecha_Orden >= hace7Dias)
                        .GroupBy(o => DbFunctions.TruncateTime(o.Fecha_Orden))
                        .Select(g => new
                        {
                            Dia = g.Key,
                            TotalVentas = g.Sum(x => x.Total)
                        })
                        .OrderBy(x => x.Dia)
                        .ToList()
                        .Select(x => new
                        {
                            Dia = x.Dia.Value.ToString("dd/MM"),
                            TotalVentas = x.TotalVentas
                        })
                        .ToList();

                    var ventasTotales = db.Ordenes.Sum(o => (decimal?)o.Total) ?? 0;

                    return Ok(new
                    {
                        TotalUsuarios = totalUsuarios,
                        UsuariosActivos = usuariosActivos,
                        UsuariosInactivos = usuariosInactivos,
                        TotalProductos = totalProductos,
                        ProductosBajoInventario = productosBajoInventario,
                        VentasTotales = ventasTotales,
                        VentasUltimaSemana = ventasUltimaSemana
                    });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
