namespace Banking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransferHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "SourceUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Transactions", "TargetUserId", c => c.Int(nullable: false));
            //AddColumn("dbo.Transactions", "User_Id1", c => c.Int());
            CreateIndex("dbo.Transactions", "SourceUserId");
            CreateIndex("dbo.Transactions", "TargetUserId");
            //CreateIndex("dbo.Transactions", "User_Id1");
            AddForeignKey("dbo.Transactions", "SourceUserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Transactions", "TargetUserId", "dbo.Users", "Id", cascadeDelete: true);
            //AddForeignKey("dbo.Transactions", "User_Id1", "dbo.Users", "Id");
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.Transactions", "User_Id1", "dbo.Users");
            DropForeignKey("dbo.Transactions", "TargetUserId", "dbo.Users");
            DropForeignKey("dbo.Transactions", "SourceUserId", "dbo.Users");
            //DropIndex("dbo.Transactions", new[] { "User_Id1" });
            DropIndex("dbo.Transactions", new[] { "TargetUserId" });
            DropIndex("dbo.Transactions", new[] { "SourceUserId" });
            //DropColumn("dbo.Transactions", "User_Id1");
            DropColumn("dbo.Transactions", "TargetUserId");
            DropColumn("dbo.Transactions", "SourceUserId");
        }
    }
}
