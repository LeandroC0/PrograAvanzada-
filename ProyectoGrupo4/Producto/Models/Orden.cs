using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Producto.Models
{
    public class Orden
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Orden_ID { get; set; }

        [Required]
        public DateTime FechaOrden { get; set; }

        [Required]
        [Range(0, 999999.99)]
        public decimal Total { get; set; }

        [ForeignKey("Producto")]
        public int Producto_ID { get; set; }
        public Producto Producto { get; set; }
    }
}