using MvcTienda.Domain.Entities;
using MvcTienda.Domain.Repositories;
using MvcTienda.Infrastructura.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MvcTienda.Infrastructura.Repositories
{
    public class ResennaRepository : IResennaRepository
    {
        private readonly AppDbContext _context;

        public ResennaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Resenna> GetAll()
        {
            return _context.Resennas
                .Include(r => r.Producto)
                .ToList();
        }

        public Resenna GetById(int id)
        {
            return _context.Resennas.Find(id);
        }

        public void Add(Resenna resenna)
        {
            _context.Resennas.Add(resenna);
        }

        public void Update(Resenna resenna)
        {
            _context.Entry(resenna).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var res = _context.Resennas.Find(id);
            if (res != null)
                _context.Resennas.Remove(res);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
