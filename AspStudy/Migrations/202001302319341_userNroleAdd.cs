namespace AspStudy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userNroleAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false,maxLength:256),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.UserName, unique: true);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        User_id = c.Int(nullable: false),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_id, t.Role_Id })
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.User_id)
                .Index(t => t.Role_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "User_id", "dbo.Users");
            DropIndex("dbo.UserRoles", new[] { "Role_Id" });
            DropIndex("dbo.UserRoles", new[] { "User_id" });
            DropIndex("dbo.Users", new[] { "UserName" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
        }
    }
}
