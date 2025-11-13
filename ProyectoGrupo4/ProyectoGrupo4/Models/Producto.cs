using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoGrupo4.Models
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }
        [Required]
        public int Inventario { get; set; }

        [ForeignKey("Estado")]
        public int EstadoId { get; set; }
        public virtual Estado Estado { get; set; }

        public virtual ICollection<ImagenProducto> Imagenes { get; set; }
        public virtual ICollection<Resenna> Resennas { get; set; }
        public virtual ICollection<DetalleOrden> DetallesOrden { get; set; }

    }
}