namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_OriginalPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "OriginalPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goods", "OriginalPrice");
        }
    }
}
