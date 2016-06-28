namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterColumn_Transaction_Payway : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Transactions", "PayWay", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transactions", "PayWay", c => c.String(nullable: false, unicode: false));
        }
    }
}
