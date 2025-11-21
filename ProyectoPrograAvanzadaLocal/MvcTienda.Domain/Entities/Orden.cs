using System;
using System.Collections.Generic;

namespace MvcTienda.Domain.Entities
{
    public class Orden
    {
        public int ID_Orden { get; set; }
        public DateTime Fecha_Orden { get; set; }
        public decimal Total { get; set; }

        public string ID_Usuario { get; set; }
        public int ID_Estado { get; set; }

        public Estado Estado { get; set; }

        public ICollection<DetalleOrden> Detalles { get; set; }
    }
}
