namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable_ExchangeRecords : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExchangeServiceRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        ExchangeSerivceId = c.Guid(nullable: false),
                        Credit = c.Double(nullable: false),
                        IsUse = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExchangeServiceRecords");
        }
    }
}
