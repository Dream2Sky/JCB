namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropColumn_Goods_CategoryId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Goods", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Goods", "CategoryId", c => c.Guid(nullable: false));
        }
    }
}
