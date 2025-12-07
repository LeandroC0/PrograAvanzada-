using MvcTienda.Infrastructura.Data;
using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity; // ✅ IMPORTANTE para DbFunctions

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
                    // ✅ TOTAL USUARIOS
                    var totalUsuarios = db.Users.Count();

                    // ✅ TOTAL PRODUCTOS
                    var totalProductos = db.Productos.Count();

                    // ✅ PRODUCTOS CON BAJO INVENTARIO
                    var productosBajoInventario = db.Productos
                        .Count(p => p.Inventario < 5);

                    // ✅ FECHA HACE 7 DÍAS (usando tu Fecha_Orden)
                    var hace7Dias = DateTime.Now.AddDays(-7);

                    // ✅ VENTAS ÚLTIMA SEMANA (FORMA COMPATIBLE CON EF)
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
                            Dia = x.Dia.Value.ToString("dd/MM"), // ✅ AQUÍ SÍ se convierte
                            TotalVentas = x.TotalVentas
                        })
                        .ToList();

                    // ✅ TOTAL VENTAS
                    var ventasTotales = db.Ordenes.Sum(o => (decimal?)o.Total) ?? 0;

                    return Ok(new
                    {
                        TotalUsuarios = totalUsuarios,
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
