using MvcTienda.Domain.Entities;
using MvcTienda.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IEnumerable<ProductoDto> GetAll()
        {
            return _repository.GetAll().Select(p => new ProductoDto
            {
                ID_Producto = p.ID_Producto,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Inventario = p.Inventario,
                ID_Estado = p.ID_Estado
            });
        }

        public ProductoDto GetById(int id)
        {
            var p = _repository.GetById(id);
            if (p == null) return null;

            return new ProductoDto
            {
                ID_Producto = p.ID_Producto,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Inventario = p.Inventario,
                ID_Estado = p.ID_Estado
            };
        }

        public void Create(ProductoDto dto)
        {
            var entity = new Producto
            {
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Inventario = dto.Inventario,
                ID_Estado = dto.ID_Estado
            };
            _repository.Add(entity);
            _repository.Save();
        }

        public void Update(ProductoDto dto)
        {
            var entity = _repository.GetById(dto.ID_Producto);
            if (entity == null) return;

            entity.Nombre = dto.Nombre;
            entity.Precio = dto.Precio;
            entity.Inventario = dto.Inventario;
            entity.ID_Estado = dto.ID_Estado;

            _repository.Update(entity);
            _repository.Save();
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
            _repository.Save();
        }
    }
}
