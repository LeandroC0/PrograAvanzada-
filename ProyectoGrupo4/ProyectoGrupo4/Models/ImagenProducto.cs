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
        public int ID_Producto { get; set; }
        public int ID_Estado { get; set; }

        [ForeignKey("ID_Estado")]
        public virtual Estado Estado { get; set; }

        [ForeignKey("ID_Producto")]
        public virtual Producto Producto { get; set; }


    }
}