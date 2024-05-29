namespace Banking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TrasnsactionContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SourceCardNumber = c.String(),
                        TargetCardNumber = c.String(),
                        Purpose = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Transactions");
        }
    }
}
