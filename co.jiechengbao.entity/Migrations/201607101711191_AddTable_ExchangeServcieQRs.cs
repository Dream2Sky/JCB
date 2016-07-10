namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable_ExchangeServcieQRs : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExchangeServiceQRs");
        }
    }
}
