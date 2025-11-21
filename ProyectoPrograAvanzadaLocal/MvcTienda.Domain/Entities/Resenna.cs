using System;

namespace MvcTienda.Domain.Entities
{
    public class Resenna
    {
        public int ID_Reseña { get; set; }
        public string Comentario { get; set; }
        public int Calificación { get; set; }
        public DateTime Fecha_Reseña { get; set; }

        public int ID_Producto { get; set; }
        public int ID_Estado { get; set; }
        public string ID_Usuario { get; set; }

        public Producto Producto { get; set; }
        public Estado Estado { get; set; }
    }
}
