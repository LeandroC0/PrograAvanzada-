using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcTienda.Domain.Entities
{
    public class Estado
    {
        public int EstadoId { get; set; }
        public string Nombre { get; set; }

        public ICollection<Producto> Productos { get; set; }
        public ICollection<Orden> Ordenes { get; set; }
        public ICollection<DetalleOrden> DetallesOrden { get; set; }
        public ICollection<ImagenProducto> ImagenesProducto { get; set; }
        public ICollection<Resenna> Resenas { get; set; }
    }
}
