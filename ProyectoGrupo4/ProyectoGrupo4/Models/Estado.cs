using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoGrupo4.Models
{
    public class Estado
    {
        [Key]
        public int ID_Estado { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
        public virtual ICollection<Orden> Ordenes { get; set; }
        public virtual ICollection<DetalleOrden> DetallesOrden { get; set; }
        public virtual ICollection<ImagenProducto> ImagenesProducto { get; set; }
        public virtual ICollection<Resenna> Resenas { get; set; }
    }
}
