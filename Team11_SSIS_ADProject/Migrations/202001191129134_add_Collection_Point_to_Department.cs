namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_Collection_Point_to_Department : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "DepartmentCollectionPoint", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departments", "DepartmentCollectionPoint");
        }
    }
}
