namespace ProyectoGrupo4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigracionY : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DetalleOrdens",
                c => new
                    {
                        ID_DetalleOrden = c.Int(nullable: false, identity: true),
                        Cantidad = c.Int(nullable: false),
                        PrecioUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ID_Producto = c.Int(nullable: false),
                        ID_Orden = c.Int(nullable: false),
                        ID_Estado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID_DetalleOrden)
                .ForeignKey("dbo.Estadoes", t => t.ID_Estado, cascadeDelete: true)
                .ForeignKey("dbo.Ordens", t => t.ID_Orden, cascadeDelete: true)
                .ForeignKey("dbo.Productoes", t => t.ID_Producto, cascadeDelete: true)
                .Index(t => t.ID_Producto)
                .Index(t => t.ID_Orden)
                .Index(t => t.ID_Estado);
            
            CreateTable(
                "dbo.Estadoes",
                c => new
                    {
                        ID_Estado = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID_Estado);
            
            CreateTable(
                "dbo.ImagenProductoes",
                c => new
                    {
                        ImagenProductoId = c.Int(nullable: false, identity: true),
                        RutaImagen = c.Binary(),
                        ID_Estado = c.Int(nullable: false),
                        ID_Producto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImagenProductoId)
                .ForeignKey("dbo.Estadoes", t => t.ID_Estado, cascadeDelete: true)
                .ForeignKey("dbo.Productoes", t => t.ID_Producto, cascadeDelete: true)
                .Index(t => t.ID_Estado)
                .Index(t => t.ID_Producto);
            
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        ID_Producto = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Inventario = c.Int(nullable: false),
                        ID_Estado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID_Producto)
                .ForeignKey("dbo.Estadoes", t => t.ID_Estado, cascadeDelete: true)
                .Index(t => t.ID_Estado);
            
            CreateTable(
                "dbo.Resennas",
                c => new
                    {
                        ID_Reseña = c.Int(nullable: false, identity: true),
                        Comentario = c.String(nullable: false, maxLength: 500),
                        Calificación = c.Int(nullable: false),
                        Fecha_Reseña = c.DateTime(nullable: false),
                        ID_Estado = c.Int(nullable: false),
                        ID_Producto = c.Int(nullable: false),
                        ID_Usuario = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID_Reseña)
                .ForeignKey("dbo.Estadoes", t => t.ID_Estado, cascadeDelete: true)
                .ForeignKey("dbo.Productoes", t => t.ID_Producto, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ID_Usuario)
                .Index(t => t.ID_Estado)
                .Index(t => t.ID_Producto)
                .Index(t => t.ID_Usuario);
            
            CreateTable(
                "dbo.Ordens",
                c => new
                    {
                        ID_Orden = c.Int(nullable: false, identity: true),
                        Fecha_Orden = c.DateTime(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ID_Usuario = c.String(maxLength: 128),
                        ID_Estado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID_Orden)
                .ForeignKey("dbo.Estadoes", t => t.ID_Estado, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ID_Usuario)
                .Index(t => t.ID_Usuario)
                .Index(t => t.ID_Estado);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetalleOrdens", "ID_Producto", "dbo.Productoes");
            DropForeignKey("dbo.DetalleOrdens", "ID_Orden", "dbo.Ordens");
            DropForeignKey("dbo.DetalleOrdens", "ID_Estado", "dbo.Estadoes");
            DropForeignKey("dbo.Ordens", "ID_Usuario", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ordens", "ID_Estado", "dbo.Estadoes");
            DropForeignKey("dbo.ImagenProductoes", "ID_Producto", "dbo.Productoes");
            DropForeignKey("dbo.Resennas", "ID_Usuario", "dbo.AspNetUsers");
            DropForeignKey("dbo.Resennas", "ID_Producto", "dbo.Productoes");
            DropForeignKey("dbo.Resennas", "ID_Estado", "dbo.Estadoes");
            DropForeignKey("dbo.Productoes", "ID_Estado", "dbo.Estadoes");
            DropForeignKey("dbo.ImagenProductoes", "ID_Estado", "dbo.Estadoes");
            DropIndex("dbo.Ordens", new[] { "ID_Estado" });
            DropIndex("dbo.Ordens", new[] { "ID_Usuario" });
            DropIndex("dbo.Resennas", new[] { "ID_Usuario" });
            DropIndex("dbo.Resennas", new[] { "ID_Producto" });
            DropIndex("dbo.Resennas", new[] { "ID_Estado" });
            DropIndex("dbo.Productoes", new[] { "ID_Estado" });
            DropIndex("dbo.ImagenProductoes", new[] { "ID_Producto" });
            DropIndex("dbo.ImagenProductoes", new[] { "ID_Estado" });
            DropIndex("dbo.DetalleOrdens", new[] { "ID_Estado" });
            DropIndex("dbo.DetalleOrdens", new[] { "ID_Orden" });
            DropIndex("dbo.DetalleOrdens", new[] { "ID_Producto" });
            DropTable("dbo.Ordens");
            DropTable("dbo.Resennas");
            DropTable("dbo.Productoes");
            DropTable("dbo.ImagenProductoes");
            DropTable("dbo.Estadoes");
            DropTable("dbo.DetalleOrdens");
        }
    }
}
