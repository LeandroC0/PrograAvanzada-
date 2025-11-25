using MvcTienda.Domain.Entities;
using System.Collections.Generic;

namespace MvcTienda.Domain.Repositories
{
    public interface IImagenProductoRepository
    {
        IEnumerable<ImagenProducto> GetAll();
        ImagenProducto GetById(int id);
        void Add(ImagenProducto imagen);
        void Update(ImagenProducto imagen);
        void Delete(int id);
        void Save();
    }
}
