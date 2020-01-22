namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDelegation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DepartmentDelegations", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.DepartmentDelegations", new[] { "DepartmentId" });
            AlterColumn("dbo.DepartmentDelegations", "DepartmentId", c => c.String(maxLength: 128));
            AlterColumn("dbo.DepartmentDelegations", "Status", c => c.Boolean(nullable: false));
            CreateIndex("dbo.DepartmentDelegations", "DepartmentId");
            AddForeignKey("dbo.DepartmentDelegations", "DepartmentId", "dbo.Departments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DepartmentDelegations", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.DepartmentDelegations", new[] { "DepartmentId" });
            AlterColumn("dbo.DepartmentDelegations", "Status", c => c.String());
            AlterColumn("dbo.DepartmentDelegations", "DepartmentId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.DepartmentDelegations", "DepartmentId");
            AddForeignKey("dbo.DepartmentDelegations", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
        }
    }
}
