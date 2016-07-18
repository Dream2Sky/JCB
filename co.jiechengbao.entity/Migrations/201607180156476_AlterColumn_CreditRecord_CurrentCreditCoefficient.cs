namespace com.jiechengbao.entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterColumn_CreditRecord_CurrentCreditCoefficient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransactionDetails", "SourceOrDestination", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransactionDetails", "SourceOrDestination");
        }
    }
}
