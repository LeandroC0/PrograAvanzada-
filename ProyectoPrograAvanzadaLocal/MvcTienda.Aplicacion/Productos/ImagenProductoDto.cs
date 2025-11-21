using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcTienda.Aplicacion.Imagenes
{
    public class ImagenProductoDto
    {
        public int ImagenProductoId { get; set; }
        public byte[] RutaImagen { get; set; }
        public int ID_Producto { get; set; }
        public int ID_Estado { get; set; }

        // Opcional
        public string EstadoNombre { get; set; }
        public string ProductoNombre { get; set; }
    }
}


