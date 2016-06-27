namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterColumn_Goods_ServiceCount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "ServiceCount", c => c.Int(nullable: false));
            DropColumn("dbo.Goods", "Count");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Goods", "Count", c => c.Int(nullable: false));
            DropColumn("dbo.Goods", "ServiceCount");
        }
    }
}
