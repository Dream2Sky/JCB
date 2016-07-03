namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable_MyServices : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.Members", "TotalCredit", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "TotalCredit");
            DropTable("dbo.MyServices");
        }
    }
}
