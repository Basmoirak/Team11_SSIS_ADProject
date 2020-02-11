namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserNameToDepartmentDelegation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DepartmentDelegations", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DepartmentDelegations", "UserName");
        }
    }
}
