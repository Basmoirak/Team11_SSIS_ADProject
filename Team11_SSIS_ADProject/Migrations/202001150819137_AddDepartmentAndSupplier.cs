namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDepartmentAndSupplier : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DepartmentCode = c.String(),
                        DepartmentName = c.String(),
                        DepartmentContactName = c.String(),
                        DepartmentPhone = c.String(),
                        DepartmentFax = c.String(),
                        DepartmentHeadName = c.String(),
                        DepartmentRepresentative = c.String(),
                        createdDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        SupplierCode = c.String(),
                        SupplierName = c.String(),
                        SupplierContactName = c.String(),
                        SupplierPhone = c.String(),
                        SupplierFax = c.String(),
                        SupplierAddress = c.String(),
                        SupplierGSTNumber = c.String(),
                        createdDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Suppliers");
            DropTable("dbo.Departments");
        }
    }
}
