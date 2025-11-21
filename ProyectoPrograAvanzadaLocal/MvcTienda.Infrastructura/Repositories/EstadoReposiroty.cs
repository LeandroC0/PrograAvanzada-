using MvcTienda.Domain.Entities;
using MvcTienda.Domain.Repositories;
using MvcTienda.Infrastructura.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MvcTienda.Infrastructura.Repositories
{
    public class EstadoRepository : IEstadoRepository
    {
        private readonly AppDbContext _context;

        public EstadoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Estado> GetAll()
        {
            return _context.Estados.ToList();
        }

        public Estado GetById(int id)
        {
            return _context.Estados.Find(id);
        }

        public void Add(Estado estado)
        {
            _context.Estados.Add(estado);
        }

        public void Update(Estado estado)
        {
            _context.Entry(estado).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var est = _context.Estados.Find(id);
            if (est != null)
                _context.Estados.Remove(est);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
