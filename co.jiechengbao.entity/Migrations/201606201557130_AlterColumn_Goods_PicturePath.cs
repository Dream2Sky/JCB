namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterColumn_Goods_PicturePath : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Goods", "PicturePath", c => c.String(nullable: false, maxLength: 255, unicode: false, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Goods", "PicturePath", c => c.String(nullable: false, unicode: false));
        }
    }
}
