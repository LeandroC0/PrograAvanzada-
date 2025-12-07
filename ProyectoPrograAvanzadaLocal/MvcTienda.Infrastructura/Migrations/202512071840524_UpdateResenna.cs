namespace MvcTienda.Infrastructura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateResenna : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Resennas", "UsuarioId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Resennas", "UsuarioId", c => c.String());
        }
    }
}
