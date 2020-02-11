namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewRole : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'7', N'Representative')

            UPDATE [dbo].[AspNetUserRoles] SET RoleId = '7' WHERE UserId = '4'
            UPDATE [dbo].[AspNetUserRoles] SET RoleId = '7' WHERE UserId = '6'
            UPDATE [dbo].[AspNetUserRoles] SET RoleId = '7' WHERE UserId = '8'
            ");
        }
        
        public override void Down()
        {
        }
    }
}
