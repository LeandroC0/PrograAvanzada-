using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcTienda.Aplicacion.Usuarios
{
    public class UsuarioAdminDto
    {
        public int UsuarioId { get; set; }
        public string Usuario { get; set; }
        public string Correo { get; set; }
        public int EstadoId { get; set; }
        public string EstadoNombre { get; set; }
    }
}
