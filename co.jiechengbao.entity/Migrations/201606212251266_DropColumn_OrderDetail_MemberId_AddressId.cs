namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropColumn_OrderDetail_MemberId_AddressId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrderDetails", "MemberId");
            DropColumn("dbo.OrderDetails", "AddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "AddressId", c => c.Guid(nullable: false));
            AddColumn("dbo.OrderDetails", "MemberId", c => c.Guid(nullable: false));
        }
    }
}
