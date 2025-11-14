using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
using System.Data;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProyectoGrupo4.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        public DateTime? UltimaConexion { get; set; }
        public string NombreUsuario { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que authenticationType debe coincidir con el valor definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar reclamaciones de usuario personalizadas aquí
            return userIdentity;
        }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        internal readonly IEnumerable ApplicationUsers;

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
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