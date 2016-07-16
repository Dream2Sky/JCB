namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropColumn_Member_Asset : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Members", "Assets");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "Assets", c => c.Double(nullable: false));
        }
    }
}
