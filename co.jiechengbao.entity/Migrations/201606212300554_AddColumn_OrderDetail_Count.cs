namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_OrderDetail_Count : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "Count", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "Count");
        }
    }
}
