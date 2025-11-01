using System.ComponentModel.DataAnnotations;

namespace Usuario.Models
{
    public class Rol
    {
        [Key]
        public int Rol_ID { get; set; }
        [Required]
        [StringLength(20)]
        public string Nombre { get; set; }
    }
}