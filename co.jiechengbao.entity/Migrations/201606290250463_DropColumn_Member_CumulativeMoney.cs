namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropColumn_Member_CumulativeMoney : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Members", "CumulativeMoney");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "CumulativeMoney", c => c.Double(nullable: false));
        }
    }
}
