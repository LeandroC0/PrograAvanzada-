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
                ID_DetalleOrden = d.ID_DetalleOrden,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                ID_Producto = d.ID_Producto,
                ID_Orden = d.ID_Orden,
                ID_Estado = d.ID_Estado
            });
        }

        public DetalleOrdenDto GetById(int id)
        {
            var d = _repo.GetById(id);
            if (d == null) return null;

            return new DetalleOrdenDto
            {
                ID_DetalleOrden = d.ID_DetalleOrden,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                ID_Producto = d.ID_Producto,
                ID_Orden = d.ID_Orden,
                ID_Estado = d.ID_Estado
            };
        }

        public void Create(DetalleOrdenDto dto)
        {
            var entity = new DetalleOrden
            {
                Cantidad = dto.Cantidad,
                PrecioUnitario = dto.PrecioUnitario,
                ID_Producto = dto.ID_Producto,
                ID_Orden = dto.ID_Orden,
                ID_Estado = dto.ID_Estado
            };

            _repo.Add(entity);
            _repo.Save();
        }

        public void Update(DetalleOrdenDto dto)
        {
            var entity = _repo.GetById(dto.ID_DetalleOrden);
            if (entity == null) return;

            entity.Cantidad = dto.Cantidad;
            entity.PrecioUnitario = dto.PrecioUnitario;
            entity.ID_Producto = dto.ID_Producto;
            entity.ID_Orden = dto.ID_Orden;
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
