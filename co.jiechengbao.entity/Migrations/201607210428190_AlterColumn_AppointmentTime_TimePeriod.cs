namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterColumn_AppointmentTime_TimePeriod : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AppointmentTimes", "TimePeriod", c => c.String(nullable: false, maxLength: 20, unicode: false, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AppointmentTimes", "TimePeriod", c => c.String(nullable: false, maxLength: 10, unicode: false, storeType: "nvarchar"));
        }
    }
}
