namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_OrderDetail_CurrentCredit_TotalCredit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "CurrentCredit", c => c.Double(nullable: false));
            AddColumn("dbo.OrderDetails", "TotalCredit", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "TotalCredit");
            DropColumn("dbo.OrderDetails", "CurrentCredit");
        }
    }
}
