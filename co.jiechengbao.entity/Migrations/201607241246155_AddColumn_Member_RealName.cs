namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Member_RealName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "RealName", c => c.String(nullable: false, maxLength: 20, unicode: false, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "RealName");
        }
    }
}
