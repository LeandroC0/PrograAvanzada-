using MvcTienda.Aplicacion.Productos;
using MvcTienda.Domain.Entities;
using MvcTienda.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MvcTienda.Aplicacion.DetallesOrden
{
    public class DetalleOrdenService : IDetalleOrdenService
    {
        private readonly IDetalleOrdenRepository _repo;

        public DetalleOrdenService(IDetalleOrdenRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<DetalleOrdenDto> GetAll()
        {
            return _repo.GetAll().Select(d => new DetalleOrdenDto
            {
                DetalleOrdenId = d.DetalleOrdenId,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                ProductoId = d.ProductoId,
                ProductoNombre = d.Producto?.Nombre,
                OrdenId = d.OrdenId,
                OrdenNombre = d.Orden?.OrdenId.ToString(),
                EstadoId = d.EstadoId,
                EstadoNombre = d.Estado?.Nombre
            });
        }

        public DetalleOrdenDto GetById(int id)
        {
            var d = _repo.GetById(id);
            if (d == null) return null;

            return new DetalleOrdenDto
            {
                DetalleOrdenId = d.DetalleOrdenId,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                ProductoId = d.ProductoId,
                ProductoNombre = d.Producto?.Nombre,
                OrdenId = d.OrdenId,
                OrdenNombre = d.Orden?.OrdenId.ToString(),
                EstadoId = d.EstadoId,
                EstadoNombre = d.Estado?.Nombre
            };
        }

        public void Create(DetalleOrdenDto dto)
        {
            var entity = new DetalleOrden
            {
                Cantidad = dto.Cantidad,
                PrecioUnitario = dto.PrecioUnitario,
                ProductoId = dto.ProductoId,
                OrdenId = dto.OrdenId,
                EstadoId = dto.EstadoId
            };

            _repo.Add(entity);
            _repo.Save();
        }

        public void Update(DetalleOrdenDto dto)
        {
            var entity = _repo.GetById(dto.DetalleOrdenId);
            if (entity == null) return;

            entity.Cantidad = dto.Cantidad;
            entity.PrecioUnitario = dto.PrecioUnitario;
            entity.ProductoId = dto.ProductoId;
            entity.OrdenId = dto.OrdenId;
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
