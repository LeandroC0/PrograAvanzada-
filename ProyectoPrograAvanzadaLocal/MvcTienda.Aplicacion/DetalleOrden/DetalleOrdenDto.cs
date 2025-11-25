namespace MvcTienda.Aplicacion.Productos
{
    public class DetalleOrdenDto
    {
        public int ID_DetalleOrden { get; set; }
        public int ID_Orden { get; set; }
        public int ID_Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public int ID_Estado { get; set; }

    }
}
