namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_NotificationsToIdentityUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Notifications", "ApplicationUser_Id");
            AddForeignKey("dbo.Notifications", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Notifications", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Notifications", "ApplicationUser_Id");
        }
    }
}
