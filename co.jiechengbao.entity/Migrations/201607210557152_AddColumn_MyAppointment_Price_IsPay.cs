namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_MyAppointment_Price_IsPay : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MyAppointments", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.MyAppointments", "IsPay", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MyAppointments", "IsPay");
            DropColumn("dbo.MyAppointments", "Price");
        }
    }
}
