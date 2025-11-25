namespace MvcTienda.Aplicacion.Productos
{
    public class ProductoDto
    {
        public int ID_Producto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Inventario { get; set; }
        public int ID_Estado { get; set; }


    }
}

