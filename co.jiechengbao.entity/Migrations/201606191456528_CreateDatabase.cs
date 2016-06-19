namespace co.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        Province = c.String(nullable: false, maxLength: 30, unicode: false, storeType: "nvarchar"),
                        City = c.String(nullable: false, maxLength: 30, unicode: false, storeType: "nvarchar"),
                        County = c.String(nullable: false, maxLength: 30, unicode: false, storeType: "nvarchar"),
                        Detail = c.String(nullable: false, maxLength: 255, unicode: false, storeType: "nvarchar"),
                        Consignee = c.String(nullable: false, maxLength: 30, unicode: false, storeType: "nvarchar"),
                        Phone = c.String(nullable: false, maxLength: 20, unicode: false, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Account = c.String(nullable: false, maxLength: 50, unicode: false, storeType: "nvarchar"),
                        Password = c.String(nullable: false, maxLength: 50, unicode: false, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        GoodsId = c.Guid(nullable: false),
                        Count = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GoodsImages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GoodsId = c.Guid(nullable: false),
                        ImagePath = c.String(nullable: false, maxLength: 255, unicode: false, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Goods",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255, unicode: false, storeType: "nvarchar"),
                        Code = c.String(nullable: false, maxLength: 10, unicode: false, storeType: "nvarchar"),
                        Price = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OpenId = c.String(nullable: false, maxLength: 50, unicode: false, storeType: "nvarchar"),
                        NickeName = c.String(maxLength: 50, unicode: false, storeType: "nvarchar"),
                        HeadImage = c.String(unicode: false),
                        Phone = c.String(maxLength: 20, unicode: false, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                        OrderNo = c.String(nullable: false, maxLength: 50, unicode: false, storeType: "nvarchar"),
                        MemberId = c.Guid(nullable: false),
                        AddressId = c.Guid(nullable: false),
                        GoodsId = c.Guid(nullable: false),
                        CurrentPrice = c.Double(nullable: false),
                        CurrentDiscount = c.Double(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderNo = c.String(nullable: false, maxLength: 50, unicode: false, storeType: "nvarchar"),
                        MemberId = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Members");
            DropTable("dbo.Goods");
            DropTable("dbo.GoodsImages");
            DropTable("dbo.Carts");
            DropTable("dbo.Admins");
            DropTable("dbo.Addresses");
        }
    }
}
