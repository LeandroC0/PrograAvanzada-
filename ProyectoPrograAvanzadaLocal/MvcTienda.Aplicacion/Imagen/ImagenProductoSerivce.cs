using MvcTienda.Domain.Entities;
using MvcTienda.Domain.Repositories;
using System;
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
                ProductoId = i.ProductoId,
                ProductoNombre = i.Producto?.Nombre,
                EstadoId = i.EstadoId,
                EstadoNombre = i.Estado != null ? i.Estado.Nombre : "Sin Estado"
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
                ProductoId = img.ProductoId,
                ProductoNombre = img.Producto?.Nombre,
                EstadoId = img.EstadoId,
                EstadoNombre = img.Estado != null ? img.Estado.Nombre : "Sin Estado"
            };
        }

        public void Create(ImagenProductoDto dto)
        {
            var entity = new ImagenProducto
            {
                RutaImagen = dto.RutaImagen,
                ProductoId = dto.ProductoId,
                EstadoId = dto.EstadoId
            };

            _repo.Add(entity);
            _repo.Save();
        }

        public void Update(ImagenProductoDto dto)
        {
            var entity = _repo.GetById(dto.ImagenProductoId);
            if (entity == null)
                throw new Exception($"No se encontró la imagen con ID: {dto.ImagenProductoId}");

            if (dto.RutaImagen != null && dto.RutaImagen.Length > 0)
            {
                entity.RutaImagen = dto.RutaImagen;
            }

            entity.ProductoId = dto.ProductoId;
            entity.EstadoId = dto.EstadoId;

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
