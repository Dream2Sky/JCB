namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Member_Assets : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Assets", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Assets");
        }
    }
}
