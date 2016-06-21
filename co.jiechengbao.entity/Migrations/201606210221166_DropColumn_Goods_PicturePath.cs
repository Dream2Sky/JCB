namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropColumn_Goods_PicturePath : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Goods", "PicturePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Goods", "PicturePath", c => c.String(nullable: false, maxLength: 255, unicode: false, storeType: "nvarchar"));
        }
    }
}
