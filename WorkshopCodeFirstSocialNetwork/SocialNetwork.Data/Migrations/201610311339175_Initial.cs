namespace SocialNetwork.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friendships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId1 = c.Int(nullable: false),
                        UserId2 = c.Int(nullable: false),
                        FriendsSince = c.DateTime(nullable: false),
                        Approved = c.Boolean(nullable: false),
                        User1_Id = c.Int(),
                        User2_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User1_Id)
                .ForeignKey("dbo.Users", t => t.User2_Id)
                .Index(t => t.Approved)
                .Index(t => t.User1_Id)
                .Index(t => t.User2_Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        SentOn = c.DateTime(nullable: false),
                        SeenOn = c.DateTime(),
                        Friendship_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Friendships", t => t.Friendship_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.SentOn)
                .Index(t => t.Friendship_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 20),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        RegisteredOn = c.DateTime(nullable: false),
                        Post_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.Post_Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(nullable: false),
                        FileExtension = c.String(nullable: false, maxLength: 4),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        PostedOn = c.DateTime(nullable: false),
                        User_Id = c.Int(),
                        User_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.User_Id1)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friendships", "User2_Id", "dbo.Users");
            DropForeignKey("dbo.Friendships", "User1_Id", "dbo.Users");
            DropForeignKey("dbo.Posts", "User_Id1", "dbo.Users");
            DropForeignKey("dbo.Posts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Messages", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Images", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "Friendship_Id", "dbo.Friendships");
            DropIndex("dbo.Posts", new[] { "User_Id1" });
            DropIndex("dbo.Posts", new[] { "User_Id" });
            DropIndex("dbo.Images", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "Post_Id" });
            DropIndex("dbo.Messages", new[] { "User_Id" });
            DropIndex("dbo.Messages", new[] { "Friendship_Id" });
            DropIndex("dbo.Messages", new[] { "SentOn" });
            DropIndex("dbo.Friendships", new[] { "User2_Id" });
            DropIndex("dbo.Friendships", new[] { "User1_Id" });
            DropIndex("dbo.Friendships", new[] { "Approved" });
            DropTable("dbo.Posts");
            DropTable("dbo.Images");
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
            DropTable("dbo.Friendships");
        }
    }
}
