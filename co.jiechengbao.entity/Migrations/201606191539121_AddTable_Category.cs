namespace co.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable_Category : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CategoryNO = c.String(nullable: false, maxLength: 20, unicode: false, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 20, unicode: false, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Categories");
        }
    }
}
