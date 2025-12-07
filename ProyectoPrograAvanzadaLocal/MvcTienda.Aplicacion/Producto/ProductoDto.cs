using System.Collections;
using System.Collections.Generic;

namespace MvcTienda.Aplicacion.Productos
{
    public class ProductoDto
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Inventario { get; set; }
        public int EstadoId { get; set; }
        public string EstadoNombre { get; set; }
        public string EstadoTexto => EstadoId == 1 ? "Activo" : "Inactivo";
        public bool EstaActivo => EstadoId == 1;
    }
}

