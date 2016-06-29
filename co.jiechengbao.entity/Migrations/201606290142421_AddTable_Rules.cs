namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable_Rules : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rules",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TotalMoney = c.Double(nullable: false),
                        VIP = c.Int(nullable: false),
                        Discount = c.Double(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Rules");
        }
    }
}
