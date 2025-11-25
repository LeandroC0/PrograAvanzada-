namespace MvcTienda.Domain.Entities
{
    public class ImagenProducto
    {
        public int ImagenProductoId { get; set; }
        public byte[] RutaImagen { get; set; }

        public int ProductoId { get; set; }
        public int EstadoId { get; set; }

        public Estado Estado { get; set; }
        public Producto Producto { get; set; }
    }
}
