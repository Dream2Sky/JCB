namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterColumn_MyAppointment_AppointmentTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MyAppointments", "AppointmentTime", c => c.String(nullable: false, maxLength: 50, unicode: false, storeType: "nvarchar"));
            DropColumn("dbo.MyAppointments", "TimePeriod");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MyAppointments", "TimePeriod", c => c.String(nullable: false, maxLength: 10, unicode: false, storeType: "nvarchar"));
            DropColumn("dbo.MyAppointments", "AppointmentTime");
        }
    }
}
