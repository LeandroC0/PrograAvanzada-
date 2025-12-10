using MvcTienda.Domain.Entities;
using System.Collections.Generic;

namespace MvcTienda.Domain.Repositories
{
    public interface IResennaRepository
    {
        IEnumerable<Resenna> GetAll();
        IEnumerable<Resenna> GetAllPublic();
        Resenna GetById(int id);
        void Add(Resenna resenna);
        void Update(Resenna resenna);
        void CambiarEstado(int id, int estadoId);
        void Delete(int id);
        void Save();
    }
}
