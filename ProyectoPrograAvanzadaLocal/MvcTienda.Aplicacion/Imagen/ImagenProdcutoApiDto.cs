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

    // Para enviar imágenes desde Postman o Angular
    public string ImagenBase64 { get; set; }

    // Para subir archivos desde un formulario
    public HttpPostedFileBase Archivo { get; set; }

    public int ProductoId { get; set; }
    public int EstadoId { get; set; }
}

}
