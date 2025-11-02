using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Producto.Models
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Producto_ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [Range(0, 99999.99)]
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        [ForeignKey("Estado")]
        public int Estado_ID { get; set; }
        public Estado Estado { get; set; }
    }
}