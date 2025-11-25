using MvcTienda.Aplicacion.Common;
using MvcTienda.Domain.Entities;
using MvcTienda.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MvcTienda.Aplicacion.Productos
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;

        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ProductoDto> GetAll()
        {
            return _repository.GetAll().Select(p => new ProductoDto
            {
                ProductoId = p.ProductoId,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Inventario = p.Inventario,
                EstadoId = p.EstadoId
            });
        }

        public ProductoDto GetById(int id)
        {
            var p = _repository.GetById(id);
            if (p == null) return null;

            return new ProductoDto
            {
                ProductoId = p.ProductoId,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Inventario = p.Inventario,
                EstadoId = p.EstadoId
            };
        }

        public void Create(ProductoDto dto)
        {
            var entity = new Producto
            {
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Inventario = dto.Inventario,
                EstadoId = dto.EstadoId
            };
            _repository.Add(entity);
            _repository.Save();
        }

        public void Update(ProductoDto dto)
        {
            var entity = _repository.GetById(dto.ProductoId);
            if (entity == null)
            {
                throw new NegocioException("No se puede acutalizar el producto.");
            }

            entity.Nombre = dto.Nombre;
            entity.Precio = dto.Precio;
            entity.Inventario = dto.Inventario;
            entity.EstadoId = dto.EstadoId;

            _repository.Update(entity);
            _repository.Save();
        }

        public void Delete(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
            {
                throw new NegocioException("No se puede eliminar el producto.");
            }
            _repository.Delete(id);
            _repository.Save();
        }
    }
}
