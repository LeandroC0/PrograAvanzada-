using System.Collections.Generic;
using System.Data.Entity;

namespace Usuario.Models
{
    public class UsuariosContext : DbContext
    {
        public UsuariosContext() : base("ProyectoG4DbConnection")
        {

        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles  { get; set; }
    }
}