using MvcTienda.Domain.Entities;
using MvcTienda.Domain.Repositories;
using MvcTienda.Infrastructura.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MvcTienda.Infrastructura.Repositories
{
    public class OrdenRepository : IOrdenRepository
    {
        private readonly AppDbContext _context;

        public OrdenRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Orden> GetAll()
        {
            return _context.Ordenes.Include(o => o.Detalles).Include(o => o.Estado).ToList();
        }

        public Orden GetById(int id)
        {
            return _context.Ordenes.Include(o => o.Estado).Include(o => o.Detalles).FirstOrDefault(o => o.OrdenId == id);
        }

        public void Add(Orden orden)
        {
            _context.Ordenes.Add(orden);
        }

        public void Update(Orden orden)
        {
            _context.Entry(orden).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var ord = _context.Ordenes.Find(id);
            if (ord != null)
                _context.Ordenes.Remove(ord);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
