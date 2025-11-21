using MvcTienda.Domain.Entities;
using MvcTienda.Domain.Repositories;
using MvcTienda.Infrastructura.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MvcTienda.Infrastructura.Repositories
{
    public class DetalleOrdenRepository : IDetalleOrdenRepository
    {
        private readonly AppDbContext _context;

        public DetalleOrdenRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DetalleOrden> GetAll()
        {
            return _context.DetallesOrden
                .Include(d => d.Producto)
                .Include(d => d.Orden)
                .ToList();
        }

        public DetalleOrden GetById(int id)
        {
            return _context.DetallesOrden
                .Include(d => d.Producto)
                .Include(d => d.Orden)
                .FirstOrDefault(d => d.ID_DetalleOrden == id);
        }

        public void Add(DetalleOrden detalle)
        {
            _context.DetallesOrden.Add(detalle);
        }

        public void Update(DetalleOrden detalle)
        {
            _context.Entry(detalle).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var det = _context.DetallesOrden.Find(id);
            if (det != null)
                _context.DetallesOrden.Remove(det);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
