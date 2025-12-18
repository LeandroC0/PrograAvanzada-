using MvcTienda.Domain.Entities;
using MvcTienda.Domain.Repositories;
using MvcTienda.Infrastructura.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MvcTienda.Infrastructura.Repositories
{
    public class ImagenProductoRepository : IImagenProductoRepository
    {
        private readonly AppDbContext _context;

        public ImagenProductoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ImagenProducto> GetAll()
        {
            return _context.ImagenesProducto
                .Include(i => i.Producto)
                .Include(i => i.Estado)
                .ToList();
        }

        public ImagenProducto GetById(int id)
        {
            return _context.ImagenesProducto
        .Include(i => i.Producto)
        .Include(i => i.Estado)
        .FirstOrDefault(i => i.ImagenProductoId == id);
        }

        public void Add(ImagenProducto imagen)
        {
            _context.ImagenesProducto.Add(imagen);
        }

        public void Update(ImagenProducto imagen)
        {
            _context.Entry(imagen).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var img = _context.ImagenesProducto.Find(id);
            if (img != null)
                _context.ImagenesProducto.Remove(img);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
