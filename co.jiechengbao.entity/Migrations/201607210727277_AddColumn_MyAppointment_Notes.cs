namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_MyAppointment_Notes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MyAppointments", "Notes", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MyAppointments", "Notes");
        }
    }
}
