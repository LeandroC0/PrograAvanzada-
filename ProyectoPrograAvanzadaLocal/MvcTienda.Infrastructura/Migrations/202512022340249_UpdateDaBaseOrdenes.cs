namespace MvcTienda.Infrastructura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDaBaseOrdenes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ordens", "UsuarioId", c => c.Int(nullable: false));
            CreateIndex("dbo.Ordens", "UsuarioId");
            AddForeignKey("dbo.Ordens", "UsuarioId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ordens", "UsuarioId", "dbo.AspNetUsers");
            DropIndex("dbo.Ordens", new[] { "UsuarioId" });
            AlterColumn("dbo.Ordens", "UsuarioId", c => c.String());
        }
    }
}
