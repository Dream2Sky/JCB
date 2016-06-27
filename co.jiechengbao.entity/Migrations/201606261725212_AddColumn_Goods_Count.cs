namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Goods_Count : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "Count", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goods", "Count");
        }
    }
}
