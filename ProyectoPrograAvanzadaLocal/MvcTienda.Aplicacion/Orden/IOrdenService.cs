using MvcTienda.Aplicacion.Carrito;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvcTienda.Aplicacion.Ordenes
{
    public interface IOrdenService
    {
        IEnumerable<OrdenDto> GetAll();
        OrdenDto GetById(int id);
        void Create(OrdenDto dto);
        void Update(OrdenDto dto);
        void Delete(int id);
        Task<int> CrearOrdenDesdeCarritoAsync(int usuarioId, IList<ItemCarritoDto> items);

    }
}

