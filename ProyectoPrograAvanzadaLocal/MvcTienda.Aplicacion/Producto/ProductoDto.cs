namespace MvcTienda.Aplicacion.Productos
{
    public class ProductoDto
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Inventario { get; set; }
        public int EstadoId { get; set; }


    }
}

