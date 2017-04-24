namespace Example.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _delete_user_username : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Authority.Users", "Name", c => c.String(nullable: false, maxLength: 50));
            DropColumn("Authority.Users", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("Authority.Users", "UserName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("Authority.Users", "Name", c => c.String());
        }
    }
}
