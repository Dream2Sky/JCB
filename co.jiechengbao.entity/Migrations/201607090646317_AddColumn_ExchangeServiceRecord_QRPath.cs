namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_ExchangeServiceRecord_QRPath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExchangeServiceRecords", "QRPath", c => c.String(nullable: false, maxLength: 255, unicode: false, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExchangeServiceRecords", "QRPath");
        }
    }
}
