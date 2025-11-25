using System.Collections.Generic;

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