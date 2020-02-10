namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedNewUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [DepartmentId]) VALUES (N'11', N'barney@email.com', 0, N'AJ83R2/+Zj2sZ1s+vjMptCdhWbls9PllsNprQqhCbsxzTlIZzagMkB5/njIEgOwZ7w==', N'467b70f2-2e2d-43b5-aa43-df05fb57d231', NULL, 0, 0, NULL, 1, 0, N'barney@email.com', N'1')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [DepartmentId]) VALUES (N'12', N'robin@email.com', 0, N'AJ83R2/+Zj2sZ1s+vjMptCdhWbls9PllsNprQqhCbsxzTlIZzagMkB5/njIEgOwZ7w==', N'467b70f2-2e2d-43b5-aa43-df05fb57d231', NULL, 0, 0, NULL, 1, 0, N'robin@email.com', N'1')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [DepartmentId]) VALUES (N'13', N'marshall@email.com', 0, N'AJ83R2/+Zj2sZ1s+vjMptCdhWbls9PllsNprQqhCbsxzTlIZzagMkB5/njIEgOwZ7w==', N'467b70f2-2e2d-43b5-aa43-df05fb57d231', NULL, 0, 0, NULL, 1, 0, N'marshall@email.com', N'2')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [DepartmentId]) VALUES (N'14', N'lily@email.com', 0, N'AJ83R2/+Zj2sZ1s+vjMptCdhWbls9PllsNprQqhCbsxzTlIZzagMkB5/njIEgOwZ7w==', N'467b70f2-2e2d-43b5-aa43-df05fb57d231', NULL, 0, 0, NULL, 1, 0, N'lily@email.com', N'2')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [DepartmentId]) VALUES (N'15', N'ted@email.com', 0, N'AJ83R2/+Zj2sZ1s+vjMptCdhWbls9PllsNprQqhCbsxzTlIZzagMkB5/njIEgOwZ7w==', N'467b70f2-2e2d-43b5-aa43-df05fb57d231', NULL, 0, 0, NULL, 1, 0, N'ted@email.com', N'3')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [DepartmentId]) VALUES (N'16', N'bob@email.com', 0, N'AJ83R2/+Zj2sZ1s+vjMptCdhWbls9PllsNprQqhCbsxzTlIZzagMkB5/njIEgOwZ7w==', N'467b70f2-2e2d-43b5-aa43-df05fb57d231', NULL, 0, 0, NULL, 1, 0, N'bob@email.com', N'3')

            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'11', N'1')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'12', N'1')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'13', N'1')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'14', N'1')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'15', N'1')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'16', N'1')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
