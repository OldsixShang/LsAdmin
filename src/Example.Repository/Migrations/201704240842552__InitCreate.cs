namespace Example.Repository.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _InitCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Authority.AuthAction",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false),
                        Template = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Authority.Menu",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        MenuType = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Url = c.String(),
                        Icon = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Authority.Permissions",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        ParentId = c.Long(),
                        Name = c.String(nullable: false),
                        MenuId = c.Long(),
                        ActionId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Authority.AuthAction", t => t.ActionId)
                .ForeignKey("Authority.Menu", t => t.MenuId)
                .ForeignKey("Authority.Permissions", t => t.ParentId)
                .Index(t => t.ParentId)
                .Index(t => t.MenuId)
                .Index(t => t.ActionId);
            
            CreateTable(
                "Authority.Roles",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(),
                        TenantId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Role_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RolesNameIndex");
            
            CreateTable(
                "Authority.Users",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(),
                        LoginId = c.String(nullable: false, maxLength: 50),
                        Password = c.String(),
                        Phone = c.String(),
                        RealName = c.String(),
                        Email = c.String(),
                        Icon = c.Binary(),
                        LoginTime = c.DateTime(),
                        RoleId = c.Long(),
                        UserName = c.String(nullable: false, maxLength: 50),
                        TenantId = c.Int(nullable: false),
                        FailuerCounts = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        LastUpdatedTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_User_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Authority.Roles", t => t.RoleId)
                .Index(t => t.LoginId, unique: true, name: "LOGINIDINDEX")
                .Index(t => t.RoleId);
            
            CreateTable(
                "Authority.Role_Permission",
                c => new
                    {
                        RoleId = c.Long(nullable: false),
                        PId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.PId })
                .ForeignKey("Authority.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("Authority.Permissions", t => t.PId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.PId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Authority.Users", "RoleId", "Authority.Roles");
            DropForeignKey("Authority.Role_Permission", "PId", "Authority.Permissions");
            DropForeignKey("Authority.Role_Permission", "RoleId", "Authority.Roles");
            DropForeignKey("Authority.Permissions", "ParentId", "Authority.Permissions");
            DropForeignKey("Authority.Permissions", "MenuId", "Authority.Menu");
            DropForeignKey("Authority.Permissions", "ActionId", "Authority.AuthAction");
            DropIndex("Authority.Role_Permission", new[] { "PId" });
            DropIndex("Authority.Role_Permission", new[] { "RoleId" });
            DropIndex("Authority.Users", new[] { "RoleId" });
            DropIndex("Authority.Users", "LOGINIDINDEX");
            DropIndex("Authority.Roles", "RolesNameIndex");
            DropIndex("Authority.Permissions", new[] { "ActionId" });
            DropIndex("Authority.Permissions", new[] { "MenuId" });
            DropIndex("Authority.Permissions", new[] { "ParentId" });
            DropTable("Authority.Role_Permission");
            DropTable("Authority.Users",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_User_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("Authority.Roles",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Role_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("Authority.Permissions");
            DropTable("Authority.Menu");
            DropTable("Authority.AuthAction");
        }
    }
}
