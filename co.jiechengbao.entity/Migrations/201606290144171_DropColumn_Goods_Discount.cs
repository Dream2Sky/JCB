namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropColumn_Goods_Discount : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Goods", "Discount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Goods", "Discount", c => c.Double(nullable: false));
        }
    }
}
