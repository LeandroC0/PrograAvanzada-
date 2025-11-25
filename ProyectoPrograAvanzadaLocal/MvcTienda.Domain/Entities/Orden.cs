using System;
using System.Collections.Generic;

namespace MvcTienda.Domain.Entities
{
    public class Orden
    {
        public int OrdenId { get; set; }
        public DateTime Fecha_Orden { get; set; }
        public decimal Total { get; set; }

        public string UsuarioId { get; set; }
        public int EstadoId { get; set; }

        public Estado Estado { get; set; }

        public ICollection<DetalleOrden> Detalles { get; set; }
    }
}
