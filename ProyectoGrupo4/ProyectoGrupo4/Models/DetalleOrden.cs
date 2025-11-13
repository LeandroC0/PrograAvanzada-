using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoGrupo4.Models
{
    public class DetalleOrden
    {
        [Key]
        public int ID_DetalleOrden { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal PrecioUnitario { get; set; }

        [ForeignKey("Producto")]
        public int ID_Producto { get; set; }
        public virtual Producto Producto { get; set; }

        [ForeignKey("Orden")]
        public int ID_Orden { get; set; }
        public virtual Orden Orden { get; set; }

        [ForeignKey("Estado")]
        public int ID_Estado { get; set; }
        public virtual Estado Estado { get; set; }
    }
}