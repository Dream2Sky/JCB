namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Member_Vip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Vip", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Vip");
        }
    }
}
