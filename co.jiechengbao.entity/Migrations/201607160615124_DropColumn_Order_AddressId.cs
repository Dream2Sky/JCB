namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropColumn_Order_AddressId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "AddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "AddressId", c => c.Guid(nullable: false));
        }
    }
}
