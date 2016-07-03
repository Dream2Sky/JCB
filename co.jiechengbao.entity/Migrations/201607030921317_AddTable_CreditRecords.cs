namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable_CreditRecords : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreditRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        OperationType = c.String(nullable: false, maxLength: 20, unicode: false, storeType: "nvarchar"),
                        Money = c.Double(nullable: false),
                        CurrentCreditCoefficient = c.Double(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CreditRecords");
        }
    }
}
