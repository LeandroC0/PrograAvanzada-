namespace MvcTienda.Infrastructura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Prueba : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DetalleOrdens",
                c => new
                    {
                        DetalleOrdenId = c.Int(nullable: false, identity: true),
                        Cantidad = c.Int(nullable: false),
                        PrecioUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductoId = c.Int(nullable: false),
                        OrdenId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DetalleOrdenId)
                .ForeignKey("dbo.Ordens", t => t.OrdenId, cascadeDelete: true)
                .ForeignKey("dbo.Estadoes", t => t.EstadoId)
                .ForeignKey("dbo.Productoes", t => t.ProductoId)
                .Index(t => t.ProductoId)
                .Index(t => t.OrdenId)
                .Index(t => t.EstadoId);
            
            CreateTable(
                "dbo.Estadoes",
                c => new
                    {
                        EstadoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.EstadoId);
            
            CreateTable(
                "dbo.ImagenProductoes",
                c => new
                    {
                        ImagenProductoId = c.Int(nullable: false, identity: true),
                        RutaImagen = c.Binary(),
                        ProductoId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImagenProductoId)
                .ForeignKey("dbo.Estadoes", t => t.EstadoId)
                .ForeignKey("dbo.Productoes", t => t.ProductoId)
                .Index(t => t.ProductoId)
                .Index(t => t.EstadoId);
            
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        ProductoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Inventario = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductoId)
                .ForeignKey("dbo.Estadoes", t => t.EstadoId)
                .Index(t => t.EstadoId);
            
            CreateTable(
                "dbo.Resennas",
                c => new
                    {
                        ResennaId = c.Int(nullable: false, identity: true),
                        Comentario = c.String(),
                        Calificación = c.Int(nullable: false),
                        Fecha_Reseña = c.DateTime(nullable: false),
                        ProductoId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        UsuarioId = c.String(),
                    })
                .PrimaryKey(t => t.ResennaId)
                .ForeignKey("dbo.Estadoes", t => t.EstadoId)
                .ForeignKey("dbo.Productoes", t => t.ProductoId)
                .Index(t => t.ProductoId)
                .Index(t => t.EstadoId);
            
            CreateTable(
                "dbo.Ordens",
                c => new
                    {
                        OrdenId = c.Int(nullable: false, identity: true),
                        Fecha_Orden = c.DateTime(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UsuarioId = c.String(),
                        EstadoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrdenId)
                .ForeignKey("dbo.Estadoes", t => t.EstadoId)
                .Index(t => t.EstadoId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodigoUsuario = c.String(),
                        FechaUltimaConexion = c.DateTime(),
                        EstadoId = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estadoes", t => t.EstadoId, cascadeDelete: true)
                .Index(t => t.EstadoId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "EstadoId", "dbo.Estadoes");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.DetalleOrdens", "ProductoId", "dbo.Productoes");
            DropForeignKey("dbo.DetalleOrdens", "EstadoId", "dbo.Estadoes");
            DropForeignKey("dbo.Ordens", "EstadoId", "dbo.Estadoes");
            DropForeignKey("dbo.DetalleOrdens", "OrdenId", "dbo.Ordens");
            DropForeignKey("dbo.ImagenProductoes", "ProductoId", "dbo.Productoes");
            DropForeignKey("dbo.Resennas", "ProductoId", "dbo.Productoes");
            DropForeignKey("dbo.Resennas", "EstadoId", "dbo.Estadoes");
            DropForeignKey("dbo.Productoes", "EstadoId", "dbo.Estadoes");
            DropForeignKey("dbo.ImagenProductoes", "EstadoId", "dbo.Estadoes");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "EstadoId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Ordens", new[] { "EstadoId" });
            DropIndex("dbo.Resennas", new[] { "EstadoId" });
            DropIndex("dbo.Resennas", new[] { "ProductoId" });
            DropIndex("dbo.Productoes", new[] { "EstadoId" });
            DropIndex("dbo.ImagenProductoes", new[] { "EstadoId" });
            DropIndex("dbo.ImagenProductoes", new[] { "ProductoId" });
            DropIndex("dbo.DetalleOrdens", new[] { "EstadoId" });
            DropIndex("dbo.DetalleOrdens", new[] { "OrdenId" });
            DropIndex("dbo.DetalleOrdens", new[] { "ProductoId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Ordens");
            DropTable("dbo.Resennas");
            DropTable("dbo.Productoes");
            DropTable("dbo.ImagenProductoes");
            DropTable("dbo.Estadoes");
            DropTable("dbo.DetalleOrdens");
        }
    }
}
