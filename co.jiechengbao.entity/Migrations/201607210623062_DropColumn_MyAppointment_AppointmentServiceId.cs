namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropColumn_MyAppointment_AppointmentServiceId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MyAppointments", "AppointmentServiceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MyAppointments", "AppointmentServiceId", c => c.Guid(nullable: false));
        }
    }
}
