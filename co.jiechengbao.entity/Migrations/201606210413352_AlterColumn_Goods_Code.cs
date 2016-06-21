namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterColumn_Goods_Code : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Goods", "Code", c => c.String(nullable: false, maxLength: 30, unicode: false, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Goods", "Code", c => c.String(nullable: false, maxLength: 10, unicode: false, storeType: "nvarchar"));
        }
    }
}
