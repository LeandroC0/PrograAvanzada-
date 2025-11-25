using System;

namespace MvcTienda.Aplicacion.Resennas
{
    public class ResennaDto
    {
        public int ID_Resenna { get; set; }
        public string Comentario { get; set; }
        public int Calificacion { get; set; }
        public DateTime Fecha_Resenna { get; set; }

        public int ID_Producto { get; set; }
        public int ID_Estado { get; set; }
        public string ID_Usuario { get; set; }


    }
}
