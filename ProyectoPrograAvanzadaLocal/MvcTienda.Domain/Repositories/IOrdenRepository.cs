using MvcTienda.Domain.Entities;
using System.Collections.Generic;

namespace MvcTienda.Domain.Repositories
{
    public interface IOrdenRepository
    {
        IEnumerable<Orden> GetAll();
        Orden GetById(int id);
        void Add(Orden orden);
        void Update(Orden orden);
        void Delete(int id);
        void Save();
    }
}
