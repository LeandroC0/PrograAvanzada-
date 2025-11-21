using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace MvcTienda.Aplicacion.Ordenes
{
    public interface IOrdenService
    {
        IEnumerable<OrdenDto> GetAll();
        OrdenDto GetById(int id);
        void Create(OrdenDto dto);
        void Update(OrdenDto dto);
        void Delete(int id);
    }
}

