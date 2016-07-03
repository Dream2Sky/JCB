namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Member_Credit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Credit", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Credit");
        }
    }
}
