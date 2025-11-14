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

        public int ID_Producto { get; set; }
        public int ID_Orden { get; set; }
        public int ID_Estado { get; set; }

        [ForeignKey("ID_Producto")]
        public virtual Producto Producto { get; set; }

        [ForeignKey("ID_Orden")]
        public virtual Orden Orden { get; set; }

        [ForeignKey("ID_Estado")]
        public virtual Estado Estado { get; set; }
    }
}