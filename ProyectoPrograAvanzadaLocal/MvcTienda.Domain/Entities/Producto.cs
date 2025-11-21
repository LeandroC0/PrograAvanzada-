using System.Collections.Generic;

namespace MvcTienda.Domain.Entities
{
    public class Producto
    {
        public int ID_Producto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Inventario { get; set; }

        public int ID_Estado { get; set; }

        public Estado Estado { get; set; }
        public ICollection<ImagenProducto> Imagenes { get; set; }
        public ICollection<Resenna> Resennas { get; set; }
        public ICollection<DetalleOrden> DetallesOrden { get; set; }
    }
}
