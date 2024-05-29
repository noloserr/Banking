namespace Banking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddToCardUserid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cards", "User_Id", "dbo.Users");
            DropIndex("dbo.Cards", new[] { "User_Id" });
            RenameColumn(table: "dbo.Cards", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Cards", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cards", "UserId");
            AddForeignKey("dbo.Cards", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "UserId", "dbo.Users");
            DropIndex("dbo.Cards", new[] { "UserId" });
            AlterColumn("dbo.Cards", "UserId", c => c.Int());
            RenameColumn(table: "dbo.Cards", name: "UserId", newName: "User_Id");
            CreateIndex("dbo.Cards", "User_Id");
            AddForeignKey("dbo.Cards", "User_Id", "dbo.Users", "Id");
        }
    }
}
