namespace Usuario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreaUsuarioYproducto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rols",
                c => new
                    {
                        Rol_ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Rol_ID);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Usuario_ID = c.Int(nullable: false, identity: true),
                        NombreUsuario = c.String(nullable: false, maxLength: 50),
                        Contrasena = c.String(nullable: false, maxLength: 100),
                        fechaUltimaConexion = c.DateTime(nullable: false),
                        Rol_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Usuario_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Usuarios");
            DropTable("dbo.Rols");
        }
    }
}
