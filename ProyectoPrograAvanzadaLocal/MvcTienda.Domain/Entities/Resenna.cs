using System;

namespace MvcTienda.Domain.Entities
{
    public class Resenna
    {
        public int ResennaId { get; set; }
        public string Comentario { get; set; }
        public int Calificación { get; set; }
        public DateTime Fecha_Reseña { get; set; }

        public int ProductoId { get; set; }
        public int EstadoId { get; set; }
        public int UsuarioId { get; set; }

        public Producto Producto { get; set; }
        public Estado Estado { get; set; }

       
    }
}
