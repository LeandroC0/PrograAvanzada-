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

        [ForeignKey("Estado")]
        public int ID_Estado { get; set; }
        public virtual Estado Estado { get; set; }

        [ForeignKey("Producto")]
        public int ID_Producto { get; set; }
        public virtual Producto Producto { get; set; }


    }
}