namespace MvcTienda.Domain.Entities
{
    public class DetalleOrden
    {
        public int ID_DetalleOrden { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        // Foreign keys
        public int ID_Producto { get; set; }
        public int ID_Orden { get; set; }
        public int ID_Estado { get; set; }

        // Navigation properties
        public Producto Producto { get; set; }
        public Orden Orden { get; set; }
        public Estado Estado { get; set; }
    }
}
