namespace IAAI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssnJobTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobtTitle = c.String(nullable: false, maxLength: 20),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AssnMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssnJobTitleId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Gender = c.String(nullable: false),
                        Incumbent = c.String(nullable: false, maxLength: 50),
                        Information = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssnJobTitles", t => t.AssnJobTitleId, cascadeDelete: true)
                .Index(t => t.AssnJobTitleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssnMembers", "AssnJobTitleId", "dbo.AssnJobTitles");
            DropIndex("dbo.AssnMembers", new[] { "AssnJobTitleId" });
            DropTable("dbo.AssnMembers");
            DropTable("dbo.AssnJobTitles");
        }
    }
}
