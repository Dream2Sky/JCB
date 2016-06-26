namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Order_AddressId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "AddressId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "AddressId");
        }
    }
}
