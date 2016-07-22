namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_MyAppointment_Description_Supplement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MyAppointments", "Description", c => c.String(nullable: false, maxLength: 255, unicode: false, storeType: "nvarchar"));
            AddColumn("dbo.MyAppointments", "Supplement", c => c.String(maxLength: 255, unicode: false, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MyAppointments", "Supplement");
            DropColumn("dbo.MyAppointments", "Description");
        }
    }
}
