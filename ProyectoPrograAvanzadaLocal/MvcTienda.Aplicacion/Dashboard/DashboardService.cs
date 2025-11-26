using System;
using System.Collections.Generic;
using System.Linq;
using MvcTienda.Aplicacion.Productos;
using MvcTienda.Aplicacion.Ordenes;
using MvcTienda.Aplicacion.Seguridad;

namespace MvcTienda.Aplicacion.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly IAuthService _authService;
        private readonly IProductoService _productoService;
        private readonly IOrdenService _ordenService;

        public DashboardService(IAuthService authService,
                              IProductoService productoService,
                              IOrdenService ordenService)
        {
            _authService = authService;
            _productoService = productoService;
            _ordenService = ordenService;
        }

        public DashboardDto ObtenerEstadisticas()
        {
            var dashboard = new DashboardDto();

            try
            {
                // Estadísticas de USUARIOS
                dashboard.TotalUsuarios = _authService.ObtenerTotalUsuarios();
                dashboard.UsuariosActivos = _authService.ObtenerUsuariosActivos();
                dashboard.UsuariosInactivos = _authService.ObtenerUsuariosInactivos();

                // Estadísticas de PRODUCTOS 
                var productos = _productoService.GetAll().ToList();
                dashboard.TotalProductos = productos.Count;
                dashboard.ProductosBajoInventario = productos.Count(p => p.Inventario < 10);

                // Estadísticas de VENTAS 
                var ordenes = _ordenService.GetAll().ToList();
                dashboard.VentasTotales = ordenes.Sum(o => o.Total);

                // Ventas última semana 
                var fechaInicioSemana = DateTime.Now.AddDays(-7);
                var ventasUltimaSemana = ordenes
                    .Where(o => o.Fecha_Orden >= fechaInicioSemana)
                    .ToList();

                dashboard.VentasUltimaSemana = new List<VentaSemanaDto>();

                // Generar datos para los últimos 7 días
                for (int i = 6; i >= 0; i--)
                {
                    var fecha = DateTime.Now.AddDays(-i).Date;
                    var ventasDelDia = ventasUltimaSemana
                        .Where(o => o.Fecha_Orden.Date == fecha)
                        .ToList();

                    dashboard.VentasUltimaSemana.Add(new VentaSemanaDto
                    {
                        Dia = fecha.ToString("dd/MM"),
                        TotalVentas = ventasDelDia.Sum(o => o.Total),
                        CantidadVentas = ventasDelDia.Count
                    });
                }
            }
            catch (Exception ex)
            {
                
                System.Diagnostics.Debug.WriteLine($"Error en DashboardService: {ex.Message}");

                // En caso de error, devolver estructura vacía pero válida
                dashboard.TotalUsuarios = 0;
                dashboard.UsuariosActivos = 0;
                dashboard.UsuariosInactivos = 0;
                dashboard.TotalProductos = 0;
                dashboard.ProductosBajoInventario = 0;
                dashboard.VentasTotales = 0;
                dashboard.VentasUltimaSemana = new List<VentaSemanaDto>
                {
                    new VentaSemanaDto { Dia = DateTime.Now.ToString("dd/MM"), TotalVentas = 0, CantidadVentas = 0 }
                };
            }

            return dashboard;
        }
    }
}