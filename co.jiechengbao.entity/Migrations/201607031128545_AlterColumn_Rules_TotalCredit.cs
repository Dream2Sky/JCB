namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterColumn_Rules_TotalCredit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rules", "TotalCredit", c => c.Double(nullable: false));
            DropColumn("dbo.Rules", "TotalMoney");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rules", "TotalMoney", c => c.Double(nullable: false));
            DropColumn("dbo.Rules", "TotalCredit");
        }
    }
}
