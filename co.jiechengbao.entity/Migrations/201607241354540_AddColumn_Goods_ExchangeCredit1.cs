namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Goods_ExchangeCredit1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "ExchangeCredit", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goods", "ExchangeCredit");
        }
    }
}
