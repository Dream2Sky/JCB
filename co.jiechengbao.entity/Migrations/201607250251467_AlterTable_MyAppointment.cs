namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTable_MyAppointment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MyAppointments", "CarNumber", c => c.String(nullable: false, maxLength: 15, unicode: false, storeType: "nvarchar"));
            AlterColumn("dbo.MyAppointments", "CarInfo", c => c.String(nullable: false, maxLength: 30, unicode: false, storeType: "nvarchar"));
            DropColumn("dbo.MyAppointments", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MyAppointments", "Description", c => c.String(nullable: false, maxLength: 255, unicode: false, storeType: "nvarchar"));
            AlterColumn("dbo.MyAppointments", "CarInfo", c => c.String(unicode: false));
            AlterColumn("dbo.MyAppointments", "CarNumber", c => c.String(unicode: false));
        }
    }
}
