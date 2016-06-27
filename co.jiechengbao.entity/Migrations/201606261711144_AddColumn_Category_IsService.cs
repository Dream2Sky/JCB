namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Category_IsService : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "IsService", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "IsService");
        }
    }
}
