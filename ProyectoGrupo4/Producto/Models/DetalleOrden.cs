using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Producto.Models
{
    public class DetalleOrden
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetalleOrden_ID { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Range(0, 99999.99)]
        public decimal PrecioUnitario { get; set; }

        [ForeignKey("Orden")]
        public int Orden_ID { get; set; }
        public Orden Orden { get; set; }

    }
}