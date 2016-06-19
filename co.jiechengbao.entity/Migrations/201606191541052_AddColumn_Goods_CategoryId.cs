namespace co.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Goods_CategoryId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "CategoryId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goods", "CategoryId");
        }
    }
}
