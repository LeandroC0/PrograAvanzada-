using MvcTienda.Aplicacion.Common;
using MvcTienda.Domain.Entities;
using MvcTienda.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTienda.Aplicacion.Productos
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;

        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductoDto> GetByIdAsync(int id)
        {

            var p = await Task.Run(() => _repository.GetById(id));
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

        public IEnumerable<ProductoDto> Search(string searchTerm, int? estadoId)
        {
            var query = _repository.GetAll();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Nombre.Contains(searchTerm));
            }

            if (estadoId.HasValue)
            {
                query = query.Where(p => p.EstadoId == estadoId.Value);
            }

            return MapToDto(query);
        }

        public IEnumerable<ProductoDto> GetAll()
        {
            var query = _repository.GetAll();
            return MapToDto(query);
        }

        private IEnumerable<ProductoDto> MapToDto(IEnumerable<Producto> productos)
        {
            return productos.Select(p => new ProductoDto
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
                EstadoId = 1
            };
            _repository.Add(entity);
            _repository.Save();
        }

        public void Update(ProductoDto dto)
        {
            var entity = _repository.GetById(dto.ProductoId);
            if (entity == null)
            {
                throw new NegocioException("No se puede actualizar el producto.");
            }

            entity.Nombre = dto.Nombre;
            entity.Precio = dto.Precio;
            entity.Inventario = dto.Inventario;
            entity.EstadoId = dto.EstadoId;

            _repository.Update(entity);
            _repository.Save();
        }

        public void ChangeStatus(int id, int estadoId)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
            {
                throw new NegocioException("No se puede cambiar el estado del producto.");
            }

            entity.EstadoId = estadoId;
            _repository.Update(entity);
            _repository.Save();
        }
    }
}
