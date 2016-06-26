namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Order_PayWay_PayTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "PayWay", c => c.String(maxLength: 10, unicode: false, storeType: "nvarchar"));
            AddColumn("dbo.Orders", "PayTime", c => c.DateTime(nullable: false, precision: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "PayTime");
            DropColumn("dbo.Orders", "PayWay");
        }
    }
}
