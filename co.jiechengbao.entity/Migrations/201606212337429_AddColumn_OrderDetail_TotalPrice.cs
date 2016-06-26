namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_OrderDetail_TotalPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "TotalPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "TotalPrice");
        }
    }
}
