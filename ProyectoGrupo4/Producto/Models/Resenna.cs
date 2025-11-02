using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Producto.Models
{
    public class Resenna
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Resenna_ID { get; set; }
        public string Comentario { get; set; }
        public int Calificacion { get; set; }
        public DateTime Fecha_Resenna { get; set; }

        [ForeignKey("Producto")]
        public int Producto_ID { get; set; }
        public Producto Producto { get; set; }

        [ForeignKey("Estado")]
        public int Estado_ID { get; set; }
        public Estado Estado { get; set; }
    }
}