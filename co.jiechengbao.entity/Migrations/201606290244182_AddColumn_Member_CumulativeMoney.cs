namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Member_CumulativeMoney : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "CumulativeMoney", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "CumulativeMoney");
        }
    }
}
