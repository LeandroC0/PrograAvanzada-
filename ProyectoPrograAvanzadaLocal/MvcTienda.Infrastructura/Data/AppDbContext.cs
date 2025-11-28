using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MvcTienda.Domain.Entities;
using MvcTienda.Infrastructura.Identity;
using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MvcTienda.Infrastructura.Data
{
    public class AppDbContext
        : IdentityDbContext<ApplicationUser, CustomRole, int,
                            CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public AppDbContext() : base("MvcTiendaProyectoG4Db")
        {
            Database.SetInitializer<AppDbContext>(null);
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
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
                .HasForeignKey(p => p.EstadoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Orden>()
                .HasRequired(o => o.Estado)
                .WithMany(e => e.Ordenes)
                .HasForeignKey(o => o.EstadoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DetalleOrden>()
                .HasRequired(d => d.Estado)
                .WithMany(e => e.DetallesOrden)
                .HasForeignKey(d => d.EstadoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ImagenProducto>()
                .HasRequired(i => i.Estado)
                .WithMany(e => e.ImagenesProducto)
                .HasForeignKey(i => i.EstadoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Resenna>()
                .HasRequired(r => r.Estado)
                .WithMany(e => e.Resenas)
                .HasForeignKey(r => r.EstadoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DetalleOrden>()
                .HasRequired(d => d.Producto)
                .WithMany(p => p.DetallesOrden)
                .HasForeignKey(d => d.ProductoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ImagenProducto>()
                .HasRequired(i => i.Producto)
                .WithMany(p => p.Imagenes)
                .HasForeignKey(i => i.ProductoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Resenna>()
                .HasRequired(r => r.Producto)
                .WithMany(p => p.Resennas)
                .HasForeignKey(r => r.ProductoId)
                .WillCascadeOnDelete(false);
        }
    }
}
