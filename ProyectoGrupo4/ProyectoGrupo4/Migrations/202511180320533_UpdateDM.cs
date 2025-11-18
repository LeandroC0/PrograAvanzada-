namespace ProyectoGrupo4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDM : DbMigration
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
                .ForeignKey("dbo.Ordens", t => t.ID_Orden, cascadeDelete: true)
                .ForeignKey("dbo.Estadoes", t => t.ID_Estado)
                .ForeignKey("dbo.Productoes", t => t.ID_Producto)
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
                        ID_Producto = c.Int(nullable: false),
                        ID_Estado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImagenProductoId)
                .ForeignKey("dbo.Estadoes", t => t.ID_Estado)
                .ForeignKey("dbo.Productoes", t => t.ID_Producto)
                .Index(t => t.ID_Producto)
                .Index(t => t.ID_Estado);
            
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
                .ForeignKey("dbo.Estadoes", t => t.ID_Estado)
                .Index(t => t.ID_Estado);
            
            CreateTable(
                "dbo.Resennas",
                c => new
                    {
                        ID_Reseña = c.Int(nullable: false, identity: true),
                        Comentario = c.String(nullable: false, maxLength: 500),
                        Calificación = c.Int(nullable: false),
                        Fecha_Reseña = c.DateTime(nullable: false),
                        ID_Producto = c.Int(nullable: false),
                        ID_Estado = c.Int(nullable: false),
                        ID_Usuario = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID_Reseña)
                .ForeignKey("dbo.Estadoes", t => t.ID_Estado)
                .ForeignKey("dbo.Productoes", t => t.ID_Producto)
                .ForeignKey("dbo.AspNetUsers", t => t.ID_Usuario)
                .Index(t => t.ID_Producto)
                .Index(t => t.ID_Estado)
                .Index(t => t.ID_Usuario);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UltimaConexion = c.DateTime(),
                        NombreUsuario = c.String(),
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
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
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
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Ordens",
                c => new
                    {
                        ID_Orden = c.Int(nullable: false, identity: true),
                        Fecha_Orden = c.DateTime(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ID_Usuario = c.String(nullable: false, maxLength: 128),
                        ID_Estado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID_Orden)
                .ForeignKey("dbo.Estadoes", t => t.ID_Estado)
                .ForeignKey("dbo.AspNetUsers", t => t.ID_Usuario)
                .Index(t => t.ID_Usuario)
                .Index(t => t.ID_Estado);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.DetalleOrdens", "ID_Producto", "dbo.Productoes");
            DropForeignKey("dbo.DetalleOrdens", "ID_Estado", "dbo.Estadoes");
            DropForeignKey("dbo.Ordens", "ID_Usuario", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ordens", "ID_Estado", "dbo.Estadoes");
            DropForeignKey("dbo.DetalleOrdens", "ID_Orden", "dbo.Ordens");
            DropForeignKey("dbo.ImagenProductoes", "ID_Producto", "dbo.Productoes");
            DropForeignKey("dbo.Resennas", "ID_Usuario", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Resennas", "ID_Producto", "dbo.Productoes");
            DropForeignKey("dbo.Resennas", "ID_Estado", "dbo.Estadoes");
            DropForeignKey("dbo.Productoes", "ID_Estado", "dbo.Estadoes");
            DropForeignKey("dbo.ImagenProductoes", "ID_Estado", "dbo.Estadoes");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Ordens", new[] { "ID_Estado" });
            DropIndex("dbo.Ordens", new[] { "ID_Usuario" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Resennas", new[] { "ID_Usuario" });
            DropIndex("dbo.Resennas", new[] { "ID_Estado" });
            DropIndex("dbo.Resennas", new[] { "ID_Producto" });
            DropIndex("dbo.Productoes", new[] { "ID_Estado" });
            DropIndex("dbo.ImagenProductoes", new[] { "ID_Estado" });
            DropIndex("dbo.ImagenProductoes", new[] { "ID_Producto" });
            DropIndex("dbo.DetalleOrdens", new[] { "ID_Estado" });
            DropIndex("dbo.DetalleOrdens", new[] { "ID_Orden" });
            DropIndex("dbo.DetalleOrdens", new[] { "ID_Producto" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Ordens");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Resennas");
            DropTable("dbo.Productoes");
            DropTable("dbo.ImagenProductoes");
            DropTable("dbo.Estadoes");
            DropTable("dbo.DetalleOrdens");
        }
    }
}
