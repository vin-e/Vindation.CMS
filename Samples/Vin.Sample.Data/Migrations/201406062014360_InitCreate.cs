namespace Vin.Sample.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Value = c.String(maxLength: 500),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Tenant_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tenants", t => t.Tenant_ID)
                .Index(t => t.Tenant_ID);
            
            CreateTable(
                "dbo.Tenants",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TenantIdentifiers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        IsDefault = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Tenant_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tenants", t => t.Tenant_ID)
                .Index(t => t.Tenant_ID);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Address = c.String(maxLength: 255),
                        City = c.String(maxLength: 100),
                        State = c.String(maxLength: 100),
                        ZipCode = c.String(maxLength: 25),
                        Details = c.String(),
                        IsPublished = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Tenant_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tenants", t => t.Tenant_ID)
                .Index(t => t.Tenant_ID);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Abbreviation = c.String(nullable: false, maxLength: 10),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Email = c.String(nullable: false, maxLength: 255),
                        Phone = c.String(maxLength: 100),
                        Message = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Tenant_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tenants", t => t.Tenant_ID)
                .Index(t => t.Tenant_ID);
            
            CreateTable(
                "dbo.Newsletters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 255),
                        OptIn = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Tenant_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tenants", t => t.Tenant_ID)
                .Index(t => t.Tenant_ID);
            
            CreateTable(
                "dbo.MediaFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Url = c.String(maxLength: 500),
                        ThumbnailUrl = c.String(maxLength: 500),
                        MediaType = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Tenant_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tenants", t => t.Tenant_ID)
                .Index(t => t.Tenant_ID);
            
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Menu_ID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Information_ID = c.Int(nullable: false),
                        ParentMenu_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MetaData", t => t.Information_ID)
                .ForeignKey("dbo.Menus", t => t.Menu_ID)
                .ForeignKey("dbo.MenuItems", t => t.ParentMenu_ID)
                .Index(t => t.Menu_ID)
                .Index(t => t.Information_ID)
                .Index(t => t.ParentMenu_ID);
            
            CreateTable(
                "dbo.MetaData",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EntityType = c.String(maxLength: 100),
                        EntityID = c.Int(nullable: false),
                        MetaKeywords = c.String(maxLength: 255),
                        MetaDescription = c.String(maxLength: 255),
                        MetaTitle = c.String(maxLength: 100),
                        Permalink = c.String(),
                        IsHomePage = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Tenant_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tenants", t => t.Tenant_ID)
                .Index(t => t.Tenant_ID);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Tenant_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tenants", t => t.Tenant_ID)
                .Index(t => t.Tenant_ID);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 100),
                        HtmlBody = c.String(),
                        TextBody = c.String(),
                        IsPublished = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Tenant_ID = c.Int(nullable: false),
                        HeaderImage_ID = c.Int(),
                        Meta_ID = c.Int(nullable: false),
                        ParentPage_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MediaFiles", t => t.HeaderImage_ID)
                .ForeignKey("dbo.MetaData", t => t.Meta_ID)
                .ForeignKey("dbo.Pages", t => t.ParentPage_ID)
                .ForeignKey("dbo.Tenants", t => t.Tenant_ID)
                .Index(t => t.Tenant_ID)
                .Index(t => t.HeaderImage_ID)
                .Index(t => t.Meta_ID)
                .Index(t => t.ParentPage_ID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Tenant_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tenants", t => t.Tenant_ID)
                .Index(t => t.Tenant_ID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 100),
                        HtmlBody = c.String(),
                        TextBody = c.String(),
                        IsPublished = c.Boolean(nullable: false),
                        PublishDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Tenant_ID = c.Int(nullable: false),
                        HeaderImage_ID = c.Int(),
                        Meta_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MediaFiles", t => t.HeaderImage_ID)
                .ForeignKey("dbo.MetaData", t => t.Meta_ID)
                .ForeignKey("dbo.Tenants", t => t.Tenant_ID)
                .Index(t => t.Tenant_ID)
                .Index(t => t.HeaderImage_ID)
                .Index(t => t.Meta_ID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Tenant_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tenants", t => t.Tenant_ID)
                .Index(t => t.Tenant_ID);
            
            CreateTable(
                "dbo.Post_Tag_Mapping",
                c => new
                    {
                        Tag_ID = c.Int(nullable: false),
                        Post_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_ID, t.Post_ID })
                .ForeignKey("dbo.Tags", t => t.Tag_ID, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_ID, cascadeDelete: true)
                .Index(t => t.Tag_ID)
                .Index(t => t.Post_ID);
            
            CreateTable(
                "dbo.Post_Category_Mapping",
                c => new
                    {
                        Category_ID = c.Int(nullable: false),
                        Post_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_ID, t.Post_ID })
                .ForeignKey("dbo.Categories", t => t.Category_ID, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_ID, cascadeDelete: true)
                .Index(t => t.Category_ID)
                .Index(t => t.Post_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "Tenant_ID", "dbo.Tenants");
            DropForeignKey("dbo.Post_Category_Mapping", "Post_ID", "dbo.Posts");
            DropForeignKey("dbo.Post_Category_Mapping", "Category_ID", "dbo.Categories");
            DropForeignKey("dbo.Posts", "Tenant_ID", "dbo.Tenants");
            DropForeignKey("dbo.Tags", "Tenant_ID", "dbo.Tenants");
            DropForeignKey("dbo.Post_Tag_Mapping", "Post_ID", "dbo.Posts");
            DropForeignKey("dbo.Post_Tag_Mapping", "Tag_ID", "dbo.Tags");
            DropForeignKey("dbo.Posts", "Meta_ID", "dbo.MetaData");
            DropForeignKey("dbo.Posts", "HeaderImage_ID", "dbo.MediaFiles");
            DropForeignKey("dbo.Pages", "Tenant_ID", "dbo.Tenants");
            DropForeignKey("dbo.Pages", "ParentPage_ID", "dbo.Pages");
            DropForeignKey("dbo.Pages", "Meta_ID", "dbo.MetaData");
            DropForeignKey("dbo.Pages", "HeaderImage_ID", "dbo.MediaFiles");
            DropForeignKey("dbo.MenuItems", "ParentMenu_ID", "dbo.MenuItems");
            DropForeignKey("dbo.MenuItems", "Menu_ID", "dbo.Menus");
            DropForeignKey("dbo.Menus", "Tenant_ID", "dbo.Tenants");
            DropForeignKey("dbo.MenuItems", "Information_ID", "dbo.MetaData");
            DropForeignKey("dbo.MetaData", "Tenant_ID", "dbo.Tenants");
            DropForeignKey("dbo.MediaFiles", "Tenant_ID", "dbo.Tenants");
            DropForeignKey("dbo.Newsletters", "Tenant_ID", "dbo.Tenants");
            DropForeignKey("dbo.Contacts", "Tenant_ID", "dbo.Tenants");
            DropForeignKey("dbo.Events", "Tenant_ID", "dbo.Tenants");
            DropForeignKey("dbo.Settings", "Tenant_ID", "dbo.Tenants");
            DropForeignKey("dbo.TenantIdentifiers", "Tenant_ID", "dbo.Tenants");
            DropIndex("dbo.Post_Category_Mapping", new[] { "Post_ID" });
            DropIndex("dbo.Post_Category_Mapping", new[] { "Category_ID" });
            DropIndex("dbo.Post_Tag_Mapping", new[] { "Post_ID" });
            DropIndex("dbo.Post_Tag_Mapping", new[] { "Tag_ID" });
            DropIndex("dbo.Tags", new[] { "Tenant_ID" });
            DropIndex("dbo.Posts", new[] { "Meta_ID" });
            DropIndex("dbo.Posts", new[] { "HeaderImage_ID" });
            DropIndex("dbo.Posts", new[] { "Tenant_ID" });
            DropIndex("dbo.Categories", new[] { "Tenant_ID" });
            DropIndex("dbo.Pages", new[] { "ParentPage_ID" });
            DropIndex("dbo.Pages", new[] { "Meta_ID" });
            DropIndex("dbo.Pages", new[] { "HeaderImage_ID" });
            DropIndex("dbo.Pages", new[] { "Tenant_ID" });
            DropIndex("dbo.Menus", new[] { "Tenant_ID" });
            DropIndex("dbo.MetaData", new[] { "Tenant_ID" });
            DropIndex("dbo.MenuItems", new[] { "ParentMenu_ID" });
            DropIndex("dbo.MenuItems", new[] { "Information_ID" });
            DropIndex("dbo.MenuItems", new[] { "Menu_ID" });
            DropIndex("dbo.MediaFiles", new[] { "Tenant_ID" });
            DropIndex("dbo.Newsletters", new[] { "Tenant_ID" });
            DropIndex("dbo.Contacts", new[] { "Tenant_ID" });
            DropIndex("dbo.Events", new[] { "Tenant_ID" });
            DropIndex("dbo.TenantIdentifiers", new[] { "Tenant_ID" });
            DropIndex("dbo.Settings", new[] { "Tenant_ID" });
            DropTable("dbo.Post_Category_Mapping");
            DropTable("dbo.Post_Tag_Mapping");
            DropTable("dbo.Tags");
            DropTable("dbo.Posts");
            DropTable("dbo.Categories");
            DropTable("dbo.Pages");
            DropTable("dbo.Menus");
            DropTable("dbo.MetaData");
            DropTable("dbo.MenuItems");
            DropTable("dbo.MediaFiles");
            DropTable("dbo.Newsletters");
            DropTable("dbo.Contacts");
            DropTable("dbo.States");
            DropTable("dbo.Events");
            DropTable("dbo.TenantIdentifiers");
            DropTable("dbo.Tenants");
            DropTable("dbo.Settings");
        }
    }
}
