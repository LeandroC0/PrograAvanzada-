using System;
using System.Collections.Generic;

namespace MvcTienda.Aplicacion.Dashboard
{
    public class DashboardDto
    {
        public int TotalUsuarios { get; set; }
        public int UsuariosActivos { get; set; }
        public int UsuariosInactivos { get; set; }
        public int TotalProductos { get; set; }
        public int ProductosBajoInventario { get; set; }
        public decimal VentasTotales { get; set; }
        public List<VentaSemanaDto> VentasUltimaSemana { get; set; }
    }

    public class VentaSemanaDto
    {
        public string Dia { get; set; }
        public decimal TotalVentas { get; set; }
        public int CantidadVentas { get; set; }
    }
}