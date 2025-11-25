namespace MvcTienda.Aplicacion.Imagenes
{
    public class ImagenProductoDto
    {
        public int ImagenProductoId { get; set; }
        public byte[] RutaImagen { get; set; }
        public int ProductoId { get; set; }
        public int EstadoId { get; set; }

        // Opcional
        public string EstadoNombre { get; set; }
        public string ProductoNombre { get; set; }
    }
}


