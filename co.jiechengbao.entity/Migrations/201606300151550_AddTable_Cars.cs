namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable_Cars : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Numberplate = c.String(nullable: false, maxLength: 20, unicode: false, storeType: "nvarchar"),
                        ChassisNumber = c.String(nullable: false, maxLength: 32, unicode: false, storeType: "nvarchar"),
                        EngineNumber = c.String(nullable: false, maxLength: 32, unicode: false, storeType: "nvarchar"),
                        CarDetailInfo = c.String(nullable: false, maxLength: 255, unicode: false, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cars");
        }
    }
}
