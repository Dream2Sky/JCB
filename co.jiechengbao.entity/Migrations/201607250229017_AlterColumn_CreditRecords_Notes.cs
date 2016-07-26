namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterColumn_CreditRecords_Notes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CreditRecords", "Notes", c => c.String(nullable: false, maxLength: 30, defaultValue: "»ý·Ö²Ù×÷"));
        }
        
        public override void Down()
        {
            
        }
    }
}
