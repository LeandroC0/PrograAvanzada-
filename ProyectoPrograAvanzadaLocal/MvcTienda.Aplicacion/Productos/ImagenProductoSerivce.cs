using MvcTienda.Domain.Entities;
using MvcTienda.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MvcTienda.Aplicacion.Imagenes
{
    public class ImagenProductoService : IImagenProductoService
    {
        private readonly IImagenProductoRepository _repo;

        public ImagenProductoService(IImagenProductoRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<ImagenProductoDto> GetAll()
        {
            return _repo.GetAll().Select(i => new ImagenProductoDto
            {
                ImagenProductoId = i.ImagenProductoId,
                RutaImagen = i.RutaImagen,
                ID_Producto = i.ID_Producto,
                ID_Estado = i.ID_Estado
            });
        }

        public ImagenProductoDto GetById(int id)
        {
            var img = _repo.GetById(id);
            if (img == null) return null;

            return new ImagenProductoDto
            {
                ImagenProductoId = img.ImagenProductoId,
                RutaImagen = img.RutaImagen,
                ID_Producto = img.ID_Producto,
                ID_Estado = img.ID_Estado
            };
        }

        public void Create(ImagenProductoDto dto)
        {
            var entity = new ImagenProducto
            {
                RutaImagen = dto.RutaImagen,
                ID_Producto = dto.ID_Producto,
                ID_Estado = dto.ID_Estado
            };

            _repo.Add(entity);
            _repo.Save();
        }

        public void Update(ImagenProductoDto dto)
        {
            var entity = _repo.GetById(dto.ImagenProductoId);
            if (entity == null) return;

            entity.RutaImagen = dto.RutaImagen;
            entity.ID_Producto = dto.ID_Producto;
            entity.ID_Estado = dto.ID_Estado;

            _repo.Update(entity);
            _repo.Save();
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
            _repo.Save();
        }
    }
}
