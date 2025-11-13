using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoGrupo4.Models
{
    public class Orden
    {
        [Key]
        public int ID_Orden { get; set; }

        [Required]
        public DateTime Fecha_Orden { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal Total { get; set; }

        [ForeignKey("Usuario")]
        public string ID_Usuario { get; set; }
        public virtual ApplicationUser Usuario { get; set; }

        [ForeignKey("Estado")]
        public int ID_Estado { get; set; }
        public virtual Estado Estado { get; set; }

        public virtual ICollection<DetalleOrden> Detalles { get; set; }

    }
}