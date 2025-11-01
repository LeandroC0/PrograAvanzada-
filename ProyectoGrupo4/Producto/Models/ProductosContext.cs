using System.Collections.Generic;
using System.Data.Entity;

namespace Producto.Models
{
    public class ProductosContext : DbContext
    {
        public ProductosContext() : base("ProyectoG4DbConnection")
        {
        }
        public DbSet<Producto> Productos { get; set; }
    }
}