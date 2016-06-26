namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Address_IsDefault : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "IsDefault", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Addresses", "IsDefault");
        }
    }
}
