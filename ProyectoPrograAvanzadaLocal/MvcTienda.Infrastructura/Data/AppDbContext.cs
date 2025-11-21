using Microsoft.AspNet.Identity.EntityFramework;
using MvcTienda.Domain.Entities;
using MvcTienda.Infrastrutura.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcTienda.Infrastructura.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection")
        {
        }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<DetalleOrden> DetallesOrden { get; set; }
        public DbSet<ImagenProducto> ImagenesProducto { get; set; }
        public DbSet<Resenna> Resennas { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Producto>()
                .HasRequired(p => p.Estado)
                .WithMany(e => e.Productos)
                .HasForeignKey(p => p.ID_Estado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Orden>()
                .HasRequired(o => o.Estado)
                .WithMany(e => e.Ordenes)
                .HasForeignKey(o => o.ID_Estado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DetalleOrden>()
                .HasRequired(d => d.Estado)
                .WithMany(e => e.DetallesOrden)
                .HasForeignKey(d => d.ID_Estado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ImagenProducto>()
                .HasRequired(i => i.Estado)
                .WithMany(e => e.ImagenesProducto)
                .HasForeignKey(i => i.ID_Estado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Resenna>()
                .HasRequired(r => r.Estado)
                .WithMany(e => e.Resenas)
                .HasForeignKey(r => r.ID_Estado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Orden>()
                .HasRequired(o => o.Usuario)
                .WithMany()
                .HasForeignKey(o => o.ID_Usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DetalleOrden>()
                .HasRequired(d => d.Producto)
                .WithMany(p => p.DetallesOrden)
                .HasForeignKey(d => d.ID_Producto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ImagenProducto>()
                .HasRequired(i => i.Producto)
                .WithMany(p => p.Imagenes)
                .HasForeignKey(i => i.ID_Producto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Resenna>()
                .HasRequired(r => r.Producto)
                .WithMany(p => p.Resennas)
                .HasForeignKey(r => r.ID_Producto)
                .WillCascadeOnDelete(false);
        }

    }
}
  