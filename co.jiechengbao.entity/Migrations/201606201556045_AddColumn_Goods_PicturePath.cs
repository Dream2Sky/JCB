namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Goods_PicturePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "PicturePath", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goods", "PicturePath");
        }
    }
}
