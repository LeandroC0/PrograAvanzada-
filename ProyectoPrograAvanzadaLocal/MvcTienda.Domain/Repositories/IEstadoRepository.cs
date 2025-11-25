using MvcTienda.Domain.Entities;
using System.Collections.Generic;

namespace MvcTienda.Domain.Repositories
{
    public interface IEstadoRepository
    {
        IEnumerable<Estado> GetAll();
        Estado GetById(int id);
        void Add(Estado estado);
        void Update(Estado estado);
        void Delete(int id);
        void Save();
    }
}
