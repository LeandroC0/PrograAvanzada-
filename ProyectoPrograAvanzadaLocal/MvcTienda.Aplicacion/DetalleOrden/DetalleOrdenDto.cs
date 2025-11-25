namespace MvcTienda.Aplicacion.Productos
{
    public class DetalleOrdenDto
    {
        public int DetalleOrdenId { get; set; }
        public int OrdenId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public int EstadoId { get; set; }

    }
}
