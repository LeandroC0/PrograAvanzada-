using MvcTienda.Domain.Entities;
using System.Collections.Generic;

namespace MvcTienda.Domain.Repositories
{
    public interface IDetalleOrdenRepository
    {
        IEnumerable<DetalleOrden> GetAll();
        DetalleOrden GetById(int id);
        void Add(DetalleOrden detalle);
        void Update(DetalleOrden detalle);
        void Delete(int id);
        void Save();
    }
}
