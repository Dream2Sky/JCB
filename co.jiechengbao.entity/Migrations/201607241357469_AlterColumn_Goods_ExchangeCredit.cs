namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterColumn_Goods_ExchangeCredit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Goods", "ExchangeCredit", c => c.Double(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Goods", "ExchangeCredit");
        }
    }
}
