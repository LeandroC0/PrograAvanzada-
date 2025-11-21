namespace MvcTienda.Domain.Entities
{
    public class ImagenProducto
    {
        public int ImagenProductoId { get; set; }
        public byte[] RutaImagen { get; set; }

        public int ID_Producto { get; set; }
        public int ID_Estado { get; set; }

        public Estado Estado { get; set; }
        public Producto Producto { get; set; }
    }
}
