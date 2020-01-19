namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [DepartmentId]) VALUES (N'1', N'storeclerk@email.com', 0, N'AJ83R2/+Zj2sZ1s+vjMptCdhWbls9PllsNprQqhCbsxzTlIZzagMkB5/njIEgOwZ7w==', N'bb97d846-7e91-4b40-a1cd-94e996ffa47c', NULL, 0, 0, NULL, 1, 0, N'storeclerk@email.com', NULL)
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [DepartmentId]) VALUES (N'2', N'storesupervisor@email.com', 0, N'AJ83R2/+Zj2sZ1s+vjMptCdhWbls9PllsNprQqhCbsxzTlIZzagMkB5/njIEgOwZ7w==', N'bb97d846-7e91-4b40-a1cd-94e996ffa47c', NULL, 0, 0, NULL, 1, 0, N'storesupervisor@email.com', NULL)
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [DepartmentId]) VALUES (N'3', N'storemanager@email.com', 0, N'AJ83R2/+Zj2sZ1s+vjMptCdhWbls9PllsNprQqhCbsxzTlIZzagMkB5/njIEgOwZ7w==', N'bb97d846-7e91-4b40-a1cd-94e996ffa47c', NULL, 0, 0, NULL, 1, 0, N'storemanager@email.com', NULL)
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [DepartmentId]) VALUES (N'4', N'joey@email.com', 0, N'AJ83R2/+Zj2sZ1s+vjMptCdhWbls9PllsNprQqhCbsxzTlIZzagMkB5/njIEgOwZ7w==', N'467b70f2-2e2d-43b5-aa43-df05fb57d231', NULL, 0, 0, NULL, 1, 0, N'joey@email.com', N'1')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [DepartmentId]) VALUES (N'5', N'ross@email.com', 0, N'AJ83R2/+Zj2sZ1s+vjMptCdhWbls9PllsNprQqhCbsxzTlIZzagMkB5/njIEgOwZ7w==', N'467b70f2-2e2d-43b5-aa43-df05fb57d231', NULL, 0, 0, NULL, 1, 0, N'ross@email.com', N'1')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [DepartmentId]) VALUES (N'6', N'rachel@email.com', 0, N'AJ83R2/+Zj2sZ1s+vjMptCdhWbls9PllsNprQqhCbsxzTlIZzagMkB5/njIEgOwZ7w==', N'467b70f2-2e2d-43b5-aa43-df05fb57d231', NULL, 0, 0, NULL, 1, 0, N'rachel@email.com', N'2')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [DepartmentId]) VALUES (N'7', N'monica@email.com', 0, N'AJ83R2/+Zj2sZ1s+vjMptCdhWbls9PllsNprQqhCbsxzTlIZzagMkB5/njIEgOwZ7w==', N'467b70f2-2e2d-43b5-aa43-df05fb57d231', NULL, 0, 0, NULL, 1, 0, N'monica@email.com', N'2')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [DepartmentId]) VALUES (N'8', N'phoebe@email.com', 0, N'AJ83R2/+Zj2sZ1s+vjMptCdhWbls9PllsNprQqhCbsxzTlIZzagMkB5/njIEgOwZ7w==', N'467b70f2-2e2d-43b5-aa43-df05fb57d231', NULL, 0, 0, NULL, 1, 0, N'phoebe@email.com', N'3')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [DepartmentId]) VALUES (N'9', N'chandler@email.com', 0, N'AJ83R2/+Zj2sZ1s+vjMptCdhWbls9PllsNprQqhCbsxzTlIZzagMkB5/njIEgOwZ7w==', N'467b70f2-2e2d-43b5-aa43-df05fb57d231', NULL, 0, 0, NULL, 1, 0, N'chandler@email.com', N'3')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [DepartmentId]) VALUES (N'10', N'admin@email.com', 0, N'AJ83R2/+Zj2sZ1s+vjMptCdhWbls9PllsNprQqhCbsxzTlIZzagMkB5/njIEgOwZ7w==', N'467b70f2-2e2d-43b5-aa43-df05fb57d231', NULL, 0, 0, NULL, 1, 0, N'admin@email.com', NULL)

            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1', N'Employee')
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2', N'DepartmentHead')
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3', N'StoreClerk')
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'4', N'StoreSupervisor')
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'5', N'StoreManager')
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'6', N'Admin')

            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1', N'3')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2', N'4')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3', N'5')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4', N'1')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5', N'2')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6', N'1')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7', N'2')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8', N'1')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9', N'2')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'10', N'6')
            ");
        }
        
        public override void Down()
        {
            Sql(@"DELETE FROM [dbo].[AspNetUsers]");
            Sql(@"DELETE FROM [dbo].[AspNetRoles]");
            Sql(@"DELETE FROM [dbo].[AspNetUserRoles]");
        }
    }
}
