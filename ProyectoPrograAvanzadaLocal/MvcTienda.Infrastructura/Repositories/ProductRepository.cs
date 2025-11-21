using MvcTienda.Domain.Entities;
using MvcTienda.Domain.Repositories;
using MvcTienda.Infrastructura.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcTienda.Infrastructura.Repositories
{
    public class ProductRepository : IProductoRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Producto> GetAll()
        {
            return _context.Productos.ToList();
        }

        public Producto GetById(int id)
        {
            return _context.Productos.Find(id);
        }

        public void Add(Producto producto)
        {
            _context.Productos.Add(producto);
        }

        public void Update(Producto producto)
        {
            _context.Entry(producto).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var prod = _context.Productos.Find(id);
            if (prod != null)
            {
                _context.Productos.Remove(prod);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}