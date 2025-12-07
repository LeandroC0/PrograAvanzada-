using System;

namespace MvcTienda.Aplicacion.Ordenes
{
    public class OrdenDto
    {
        public int OrdenId { get; set; }
        public DateTime Fecha_Orden { get; set; }
        public decimal Total { get; set; }
        public int UsuarioId { get; set; }
        public string UsuarioNombre { get; set; }
        public int EstadoId { get; set; }
        public string EstadoNombre { get; set; }
    }
}
