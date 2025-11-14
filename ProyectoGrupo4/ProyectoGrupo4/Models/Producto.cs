using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoGrupo4.Models
{
    public class Producto
    {
        [Key]
        public int ID_Producto { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        [Column(TypeName = "decimal")]
        public decimal Precio { get; set; }
        [Required]
        public int Inventario { get; set; }
        public int ID_Estado { get; set; }

        [ForeignKey("ID_Estado")]
        public virtual Estado Estado { get; set; }

        public virtual ICollection<ImagenProducto> Imagenes { get; set; }
        public virtual ICollection<Resenna> Resennas { get; set; }
        public virtual ICollection<DetalleOrden> DetallesOrden { get; set; }

    }
}