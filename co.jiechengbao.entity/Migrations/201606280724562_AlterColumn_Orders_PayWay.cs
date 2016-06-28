namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterColumn_Orders_PayWay : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "PayWay", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "PayWay", c => c.String(maxLength: 10, unicode: false, storeType: "nvarchar"));
        }
    }
}
