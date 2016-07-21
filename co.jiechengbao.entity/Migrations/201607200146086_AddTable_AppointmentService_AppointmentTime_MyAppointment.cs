namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable_AppointmentService_AppointmentTime_MyAppointment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppointmentServices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20, unicode: false, storeType: "nvarchar"),
                        Code = c.String(nullable: false, maxLength: 20, unicode: false, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppointmentTimes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TimePeriod = c.String(nullable: false, maxLength: 10, unicode: false, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MyAppointments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        AppointmentServiceId = c.Guid(nullable: false),
                        TimePeriod = c.String(nullable: false, maxLength: 10, unicode: false, storeType: "nvarchar"),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MyAppointments");
            DropTable("dbo.AppointmentTimes");
            DropTable("dbo.AppointmentServices");
        }
    }
}
