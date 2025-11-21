using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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