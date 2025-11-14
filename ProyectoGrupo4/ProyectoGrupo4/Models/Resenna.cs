using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoGrupo4.Models
{
    public class Resenna
    {
        [Key]
        public int ID_Reseña { get; set; }

        [Required, StringLength(500)]
        public string Comentario { get; set; }

        [Range(1, 5)]
        public int Calificación { get; set; }

        [Required]
        public DateTime Fecha_Reseña { get; set; }
        public int ID_Producto { get; set; }
        public int ID_Estado { get; set; }

        public string ID_Usuario { get; set; }

        [ForeignKey("ID_Producto")]
        public virtual Producto Producto { get; set; }

        [ForeignKey("ID_Estado")]
        public virtual Estado Estado { get; set; }

        [ForeignKey("ID_Usuario")]
        public virtual ApplicationUser Usuario { get; set; }
    }
}