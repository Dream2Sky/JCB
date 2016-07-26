namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterColumn_Member_RealName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "RealName", c => c.String(maxLength: 20, unicode: false, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "RealName", c => c.String(nullable: false, maxLength: 20, unicode: false, storeType: "nvarchar"));
        }
    }
}
