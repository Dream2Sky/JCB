namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_CreditRecord_Notes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CreditRecords", "Notes", c => c.String(nullable: false, maxLength: 30, unicode: false, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CreditRecords", "Notes");
        }
    }
}
