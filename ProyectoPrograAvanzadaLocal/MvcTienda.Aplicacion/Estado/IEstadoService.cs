using System.Collections.Generic;

namespace MvcTienda.Aplicacion.Estados
{
    public interface IEstadoService
    {
        IEnumerable<EstadoDto> GetAll();
        EstadoDto GetById(int id);
        void Create(EstadoDto estado);
        void Update(EstadoDto estado);
        void Delete(int id);
    }
}