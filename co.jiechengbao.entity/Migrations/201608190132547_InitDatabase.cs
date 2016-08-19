namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabase : DbMigration
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
                        IsDefault = c.Boolean(nullable: false),
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
                "dbo.Advertisements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AdCode = c.String(nullable: false, maxLength: 35, unicode: false, storeType: "nvarchar"),
                        CategoryId = c.Guid(nullable: false),
                        AdName = c.String(nullable: false, maxLength: 30, unicode: false, storeType: "nvarchar"),
                        AdDescription = c.String(nullable: false, maxLength: 255, unicode: false, storeType: "nvarchar"),
                        AdImagePath = c.String(nullable: false, maxLength: 255, unicode: false, storeType: "nvarchar"),
                        IsRecommend = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppointmentServices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20, unicode: false, storeType: "nvarchar"),
                        Code = c.String(nullable: false, maxLength: 20, unicode: false, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppointmentTimes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TimePeriod = c.String(nullable: false, maxLength: 20, unicode: false, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        Numberplate = c.String(nullable: false, maxLength: 20, unicode: false, storeType: "nvarchar"),
                        ChassisNumber = c.String(nullable: false, maxLength: 32, unicode: false, storeType: "nvarchar"),
                        EngineNumber = c.String(nullable: false, maxLength: 32, unicode: false, storeType: "nvarchar"),
                        CarDetailInfo = c.String(nullable: false, maxLength: 255, unicode: false, storeType: "nvarchar"),
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
                "dbo.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CategoryNO = c.String(nullable: false, maxLength: 20, unicode: false, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 20, unicode: false, storeType: "nvarchar"),
                        IsService = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CreditRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        OperationType = c.String(nullable: false, maxLength: 20, unicode: false, storeType: "nvarchar"),
                        Money = c.Double(nullable: false),
                        CurrentCreditCoefficient = c.Double(nullable: false),
                        Notes = c.String(nullable: false, maxLength: 30, unicode: false, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExchangeServiceRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        ExchangeSerivceId = c.Guid(nullable: false),
                        Credit = c.Double(nullable: false),
                        QRPath = c.String(nullable: false, maxLength: 255, unicode: false, storeType: "nvarchar"),
                        IsUse = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExchangeServiceQRs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        ExchangeServiceId = c.Guid(nullable: false),
                        QRPath = c.String(nullable: false, maxLength: 255, unicode: false, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExchangeServices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false, storeType: "nvarchar"),
                        ImagePath = c.String(nullable: false, maxLength: 255, unicode: false, storeType: "nvarchar"),
                        Credit = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        Notes = c.String(maxLength: 255, unicode: false, storeType: "nvarchar"),
                        Code = c.String(nullable: false, unicode: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FreeCoupons",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CouponCode = c.String(nullable: false, maxLength: 35, unicode: false, storeType: "nvarchar"),
                        CouponName = c.String(nullable: false, maxLength: 50, unicode: false, storeType: "nvarchar"),
                        Price = c.Double(nullable: false),
                        Description = c.String(maxLength: 255, unicode: false, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GoodsCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GoodsId = c.Guid(nullable: false),
                        CategoryId = c.Guid(nullable: false),
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
                        Code = c.String(nullable: false, maxLength: 30, unicode: false, storeType: "nvarchar"),
                        Price = c.Double(nullable: false),
                        Description = c.String(maxLength: 255, unicode: false, storeType: "nvarchar"),
                        ServiceCount = c.Int(nullable: false),
                        OriginalPrice = c.Double(nullable: false),
                        ExchangeCredit = c.Double(nullable: false),
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
                        RealName = c.String(maxLength: 20, unicode: false, storeType: "nvarchar"),
                        Vip = c.Int(nullable: false),
                        Credit = c.Double(nullable: false),
                        TotalCredit = c.Double(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MyAppointmentItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MyAppointmentId = c.Guid(nullable: false),
                        AppointmentServiceId = c.Guid(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MyAppointments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        AppointmentTime = c.String(nullable: false, maxLength: 50, unicode: false, storeType: "nvarchar"),
                        Price = c.Double(nullable: false),
                        IsPay = c.Boolean(nullable: false),
                        Supplement = c.String(maxLength: 255, unicode: false, storeType: "nvarchar"),
                        Notes = c.String(unicode: false),
                        CarNumber = c.String(nullable: false, maxLength: 15, unicode: false, storeType: "nvarchar"),
                        CarInfo = c.String(nullable: false, maxLength: 30, unicode: false, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MyFreeCoupons",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FreeCouponId = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        FreeCouponQRs = c.String(maxLength: 255, unicode: false, storeType: "nvarchar"),
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
                        GoodsId = c.Guid(nullable: false),
                        Count = c.Int(nullable: false),
                        CurrentPrice = c.Double(nullable: false),
                        CurrentDiscount = c.Double(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        CurrentCredit = c.Double(nullable: false),
                        TotalCredit = c.Double(nullable: false),
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
                        PayWay = c.Int(nullable: false),
                        PayTime = c.DateTime(nullable: false, precision: 0),
                        Status = c.Int(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Recharges",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        Amount = c.Double(nullable: false),
                        Payway = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReCommends",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GoodsId = c.Guid(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rules",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TotalCredit = c.Double(nullable: false),
                        VIP = c.Int(nullable: false),
                        Discount = c.Double(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceConsumePasswords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Password = c.String(nullable: false, maxLength: 32, unicode: false, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceConsumeRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ServiceId = c.Guid(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceQRs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        ServcieId = c.Guid(nullable: false),
                        QRPath = c.String(nullable: false, maxLength: 255, unicode: false, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MyServices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        GoodsId = c.Guid(nullable: false),
                        GoodsName = c.String(nullable: false, maxLength: 255, unicode: false, storeType: "nvarchar"),
                        CurrentCount = c.Int(nullable: false),
                        TotalCount = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TransactionDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        Notes = c.String(nullable: false, maxLength: 30, unicode: false, storeType: "nvarchar"),
                        Credit = c.Double(nullable: false),
                        SourceOrDestination = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        Amount = c.Double(nullable: false),
                        PayWay = c.Int(nullable: false),
                        OrderId = c.Guid(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Transactions");
            DropTable("dbo.TransactionDetails");
            DropTable("dbo.MyServices");
            DropTable("dbo.ServiceQRs");
            DropTable("dbo.ServiceConsumeRecords");
            DropTable("dbo.ServiceConsumePasswords");
            DropTable("dbo.Rules");
            DropTable("dbo.ReCommends");
            DropTable("dbo.Recharges");
            DropTable("dbo.OrderStatus");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.MyFreeCoupons");
            DropTable("dbo.MyAppointments");
            DropTable("dbo.MyAppointmentItems");
            DropTable("dbo.Members");
            DropTable("dbo.Goods");
            DropTable("dbo.GoodsImages");
            DropTable("dbo.GoodsCategories");
            DropTable("dbo.FreeCoupons");
            DropTable("dbo.ExchangeServices");
            DropTable("dbo.ExchangeServiceQRs");
            DropTable("dbo.ExchangeServiceRecords");
            DropTable("dbo.CreditRecords");
            DropTable("dbo.Categories");
            DropTable("dbo.Carts");
            DropTable("dbo.Cars");
            DropTable("dbo.AppointmentTimes");
            DropTable("dbo.AppointmentServices");
            DropTable("dbo.Advertisements");
            DropTable("dbo.Admins");
            DropTable("dbo.Addresses");
        }
    }
}
