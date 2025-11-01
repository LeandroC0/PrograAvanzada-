using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Usuario.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Usuario_ID { get; set; }
        [Required]
        [StringLength(50)]
        public string NombreUsuario { get; set; }
        [Required]
        [StringLength(100)]
        public string Contrasena { get; set; }
        [Required]
        public DateTime fechaUltimaConexion { get; set; }
        public int Rol_ID { get; set; }
    }
}