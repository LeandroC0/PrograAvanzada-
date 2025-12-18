using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MvcTienda.Aplicacion.Imagen
{
   public class ImagenProductoApiDto
{
    public int ImagenProductoId { get; set; }

   
    public string ImagenBase64 { get; set; }

    
    public HttpPostedFileBase Archivo { get; set; }

    public int ProductoId { get; set; }
    public int EstadoId { get; set; }
}

}
