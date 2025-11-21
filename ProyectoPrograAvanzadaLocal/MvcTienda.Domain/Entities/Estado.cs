using System.Collections.Generic;

namespace MvcTienda.Domain.Entities
{
    public class Estado
    {
        public int ID_Estado { get; set; }
        public string Nombre { get; set; }

        public ICollection<Producto> Productos { get; set; }
        public ICollection<Orden> Ordenes { get; set; }
        public ICollection<DetalleOrden> DetallesOrden { get; set; }
        public ICollection<ImagenProducto> ImagenesProducto { get; set; }
        public ICollection<Resenna> Resenas { get; set; }
    }
}
