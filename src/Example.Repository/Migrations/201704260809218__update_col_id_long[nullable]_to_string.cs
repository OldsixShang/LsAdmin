namespace Example.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _update_col_id_longnullable_to_string : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Authority.Permissions", "ActionId", "Authority.AuthAction");
            DropForeignKey("Authority.Permissions", "MenuId", "Authority.Menu");
            DropForeignKey("Authority.Permissions", "ParentId", "Authority.Permissions");
            DropForeignKey("Authority.Role_Permission", "PId", "Authority.Permissions");
            DropForeignKey("Authority.Role_Permission", "RoleId", "Authority.Roles");
            DropForeignKey("Authority.Users", "RoleId", "Authority.Roles");
            DropIndex("Authority.Permissions", new[] { "ParentId" });
            DropIndex("Authority.Permissions", new[] { "MenuId" });
            DropIndex("Authority.Permissions", new[] { "ActionId" });
            DropIndex("Authority.Users", new[] { "RoleId" });
            DropIndex("Authority.Role_Permission", new[] { "RoleId" });
            DropIndex("Authority.Role_Permission", new[] { "PId" });
            DropPrimaryKey("Authority.AuthAction");
            DropPrimaryKey("Authority.Menu");
            DropPrimaryKey("Authority.Permissions");
            DropPrimaryKey("Authority.Roles");
            DropPrimaryKey("Authority.Users");
            DropPrimaryKey("Authority.Role_Permission");
            AlterColumn("Authority.AuthAction", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("Authority.Menu", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("Authority.Permissions", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("Authority.Permissions", "ParentId", c => c.String(maxLength: 128));
            AlterColumn("Authority.Permissions", "MenuId", c => c.String(maxLength: 128));
            AlterColumn("Authority.Permissions", "ActionId", c => c.String(maxLength: 128));
            AlterColumn("Authority.Roles", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("Authority.Users", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("Authority.Users", "RoleId", c => c.String(maxLength: 128));
            AlterColumn("Authority.Role_Permission", "RoleId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("Authority.Role_Permission", "PId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("Authority.AuthAction", "Id");
            AddPrimaryKey("Authority.Menu", "Id");
            AddPrimaryKey("Authority.Permissions", "Id");
            AddPrimaryKey("Authority.Roles", "Id");
            AddPrimaryKey("Authority.Users", "Id");
            AddPrimaryKey("Authority.Role_Permission", new[] { "RoleId", "PId" });
            CreateIndex("Authority.Permissions", "ParentId");
            CreateIndex("Authority.Permissions", "MenuId");
            CreateIndex("Authority.Permissions", "ActionId");
            CreateIndex("Authority.Users", "RoleId");
            CreateIndex("Authority.Role_Permission", "RoleId");
            CreateIndex("Authority.Role_Permission", "PId");
            AddForeignKey("Authority.Permissions", "ActionId", "Authority.AuthAction", "Id");
            AddForeignKey("Authority.Permissions", "MenuId", "Authority.Menu", "Id");
            AddForeignKey("Authority.Permissions", "ParentId", "Authority.Permissions", "Id");
            AddForeignKey("Authority.Role_Permission", "PId", "Authority.Permissions", "Id", cascadeDelete: true);
            AddForeignKey("Authority.Role_Permission", "RoleId", "Authority.Roles", "Id", cascadeDelete: true);
            AddForeignKey("Authority.Users", "RoleId", "Authority.Roles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("Authority.Users", "RoleId", "Authority.Roles");
            DropForeignKey("Authority.Role_Permission", "RoleId", "Authority.Roles");
            DropForeignKey("Authority.Role_Permission", "PId", "Authority.Permissions");
            DropForeignKey("Authority.Permissions", "ParentId", "Authority.Permissions");
            DropForeignKey("Authority.Permissions", "MenuId", "Authority.Menu");
            DropForeignKey("Authority.Permissions", "ActionId", "Authority.AuthAction");
            DropIndex("Authority.Role_Permission", new[] { "PId" });
            DropIndex("Authority.Role_Permission", new[] { "RoleId" });
            DropIndex("Authority.Users", new[] { "RoleId" });
            DropIndex("Authority.Permissions", new[] { "ActionId" });
            DropIndex("Authority.Permissions", new[] { "MenuId" });
            DropIndex("Authority.Permissions", new[] { "ParentId" });
            DropPrimaryKey("Authority.Role_Permission");
            DropPrimaryKey("Authority.Users");
            DropPrimaryKey("Authority.Roles");
            DropPrimaryKey("Authority.Permissions");
            DropPrimaryKey("Authority.Menu");
            DropPrimaryKey("Authority.AuthAction");
            AlterColumn("Authority.Role_Permission", "PId", c => c.Long(nullable: false));
            AlterColumn("Authority.Role_Permission", "RoleId", c => c.Long(nullable: false));
            AlterColumn("Authority.Users", "RoleId", c => c.Long());
            AlterColumn("Authority.Users", "Id", c => c.Long(nullable: false));
            AlterColumn("Authority.Roles", "Id", c => c.Long(nullable: false));
            AlterColumn("Authority.Permissions", "ActionId", c => c.Long());
            AlterColumn("Authority.Permissions", "MenuId", c => c.Long());
            AlterColumn("Authority.Permissions", "ParentId", c => c.Long());
            AlterColumn("Authority.Permissions", "Id", c => c.Long(nullable: false));
            AlterColumn("Authority.Menu", "Id", c => c.Long(nullable: false));
            AlterColumn("Authority.AuthAction", "Id", c => c.Long(nullable: false));
            AddPrimaryKey("Authority.Role_Permission", new[] { "RoleId", "PId" });
            AddPrimaryKey("Authority.Users", "Id");
            AddPrimaryKey("Authority.Roles", "Id");
            AddPrimaryKey("Authority.Permissions", "Id");
            AddPrimaryKey("Authority.Menu", "Id");
            AddPrimaryKey("Authority.AuthAction", "Id");
            CreateIndex("Authority.Role_Permission", "PId");
            CreateIndex("Authority.Role_Permission", "RoleId");
            CreateIndex("Authority.Users", "RoleId");
            CreateIndex("Authority.Permissions", "ActionId");
            CreateIndex("Authority.Permissions", "MenuId");
            CreateIndex("Authority.Permissions", "ParentId");
            AddForeignKey("Authority.Users", "RoleId", "Authority.Roles", "Id");
            AddForeignKey("Authority.Role_Permission", "RoleId", "Authority.Roles", "Id", cascadeDelete: true);
            AddForeignKey("Authority.Role_Permission", "PId", "Authority.Permissions", "Id", cascadeDelete: true);
            AddForeignKey("Authority.Permissions", "ParentId", "Authority.Permissions", "Id");
            AddForeignKey("Authority.Permissions", "MenuId", "Authority.Menu", "Id");
            AddForeignKey("Authority.Permissions", "ActionId", "Authority.AuthAction", "Id");
        }
    }
}
