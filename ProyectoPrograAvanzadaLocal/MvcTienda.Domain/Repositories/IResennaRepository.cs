using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcTienda.Domain.Entities;
using System.Collections.Generic;

namespace MvcTienda.Domain.Repositories
{
    public interface IResennaRepository
    {
        IEnumerable<Resenna> GetAll();
        Resenna GetById(int id);
        void Add(Resenna resenna);
        void Update(Resenna resenna);
        void Delete(int id);
        void Save();
    }
}
