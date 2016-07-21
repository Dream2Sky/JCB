namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable_MyAppointmentItems : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyAppointmentItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MyAppointmentId = c.Guid(nullable: false),
                        AppointmentServiceId = c.Guid(nullable: false),
                        CreatedTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MyAppointmentItems");
        }
    }
}
