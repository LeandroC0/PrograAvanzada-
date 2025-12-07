using System;

namespace MvcTienda.Aplicacion.Resennas
{
    public class ResennaDto
    {
        public int ResennaId { get; set; }
        public string Comentario { get; set; }
        public int Calificacion { get; set; }
        public DateTime Fecha_Resenna { get; set; }
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; }
        public int EstadoId { get; set; }
        public string EstadoNombre { get; set; }
        public int UsuarioId { get; set; }
        public string UsuarioNombre { get; set; }
    }
}
