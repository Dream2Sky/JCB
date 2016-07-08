namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable_ExchangeServices : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExchangeServices");
        }
    }
}
