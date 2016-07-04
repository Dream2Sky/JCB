namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable_ServcieConsumePassword : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ServiceConsumePasswords");
        }
    }
}
