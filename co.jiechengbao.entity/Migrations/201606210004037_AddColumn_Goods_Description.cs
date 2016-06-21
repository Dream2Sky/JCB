namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Goods_Description : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "Description", c => c.String(maxLength: 255, unicode: false, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goods", "Description");
        }
    }
}
