namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterColumn_Recharge_Payway : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Recharges", "Payway", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recharges", "Payway", c => c.String(nullable: false, unicode: false));
        }
    }
}
