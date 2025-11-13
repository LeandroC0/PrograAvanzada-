using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoGrupo4.Models
{
    public class ImagenProducto
    {
        [Key]
        public int ImagenProductoId { get; set; }

        public byte[] RutaImagen { get; set; }
        [Required]
        public int ProductoId { get; set; }

        [ForeignKey("Estado")]
        public int EstadoId { get; set; }
        public virtual Estado Estado { get; set; }

        [ForeignKey("Producto")]
        public virtual Producto Producto { get; set; }


    }
}