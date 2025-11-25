using Microsoft.AspNet.Identity.EntityFramework;
using MvcTienda.Domain.Entities;
using MvcTienda.Infrastructura.Identity;
using System.Data.Entity;

namespace MvcTienda.Infrastructura.Data
{
    public class AppDbContext
        : IdentityDbContext<ApplicationUser, CustomRole, int,
                            CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public AppDbContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new AppDbInitalizer());
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
                .WithMany()
                .HasForeignKey(p => p.ID_Estado)
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

            modelBuilder.Entity<DetalleOrden>()
                .HasRequired(d => d.Producto)
                .WithMany(p => p.DetallesOrden)
                .HasForeignKey(d => d.ID_Producto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DetalleOrden>()
                .HasRequired(d => d.Orden)
                .WithMany(o => o.Detalles)
                .HasForeignKey(d => d.ID_Orden)
                .WillCascadeOnDelete(false);
        }
    }
}
