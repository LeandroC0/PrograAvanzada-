namespace MvcTienda.Domain.Entities
{
    public class DetalleOrden
    {
        public int DetalleOrdenId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        // Foreign keys
        public int ProductoId { get; set; }
        public int OrdenId { get; set; }
        public int EstadoId { get; set; }

        // Navigation properties
        public Producto Producto { get; set; }
        public Orden Orden { get; set; }
        public Estado Estado { get; set; }
    }
}
