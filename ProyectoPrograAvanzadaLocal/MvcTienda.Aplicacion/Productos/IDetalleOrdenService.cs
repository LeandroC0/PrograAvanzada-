using MvcTienda.Aplicacion.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
