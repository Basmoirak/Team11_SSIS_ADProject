namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_departmentDelegation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DepartmentDelegations", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DepartmentDelegations", "Status", c => c.String());
        }
    }
}
