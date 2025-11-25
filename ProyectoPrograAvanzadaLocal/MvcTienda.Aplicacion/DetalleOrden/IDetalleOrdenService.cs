using MvcTienda.Aplicacion.Productos;
using System.Collections.Generic;

namespace MvcTienda.Aplicacion.DetallesOrden
{
    public interface IDetalleOrdenService
    {
        IEnumerable<DetalleOrdenDto> GetAll();
        DetalleOrdenDto GetById(int id);
        void Create(DetalleOrdenDto dto);
        void Update(DetalleOrdenDto dto);
        void Delete(int id);
    }
}
