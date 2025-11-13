namespace ProyectoGrupo4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDataBase2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UltimaConexion", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "NombreUsuario", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "NombreUsuario");
            DropColumn("dbo.AspNetUsers", "UltimaConexion");
        }
    }
}
