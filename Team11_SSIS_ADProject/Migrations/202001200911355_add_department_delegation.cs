namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_department_delegation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DepartmentDelegations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        DepartmentId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false),
                        Status = c.String(),
                        createdDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DepartmentDelegations", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.DepartmentDelegations", new[] { "DepartmentId" });
            DropTable("dbo.DepartmentDelegations");
        }
    }
}
