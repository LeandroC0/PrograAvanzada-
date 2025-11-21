using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcTienda.Aplicacion.Imagenes
{
    public interface IImagenProductoService
    {
        IEnumerable<ImagenProductoDto> GetAll();
        ImagenProductoDto GetById(int id);
        void Create(ImagenProductoDto dto);
        void Update(ImagenProductoDto dto);
        void Delete(int id);
    }
}