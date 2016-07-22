namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_MyAppointment_CarNumber_CarInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MyAppointments", "CarNumber", c => c.String(unicode: false));
            AddColumn("dbo.MyAppointments", "CarInfo", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MyAppointments", "CarInfo");
            DropColumn("dbo.MyAppointments", "CarNumber");
        }
    }
}
