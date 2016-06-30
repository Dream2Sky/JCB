namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Cars_MemberId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "MemberId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "MemberId");
        }
    }
}
