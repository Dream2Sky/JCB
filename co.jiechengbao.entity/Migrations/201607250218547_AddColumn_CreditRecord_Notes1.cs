namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_CreditRecord_Notes1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CreditRecords", "Notes", c => c.String(nullable: false, maxLength: 30, defaultValue:"»ý·Ö²Ù×÷"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CreditRecords", "Notes");
        }
    }
}
