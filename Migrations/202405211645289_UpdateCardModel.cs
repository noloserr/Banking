namespace Banking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCardModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cards", "CVV", c => c.String());
            AlterColumn("dbo.Cards", "ExpiryDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cards", "ExpiryDate", c => c.String());
            DropColumn("dbo.Cards", "CVV");
            DropColumn("dbo.Cards", "IsActive");
        }
    }
}
