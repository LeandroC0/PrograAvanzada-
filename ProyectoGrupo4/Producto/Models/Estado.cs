using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Producto.Models
{
    public class Estado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Estado_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
    }
}