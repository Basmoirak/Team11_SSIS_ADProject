namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResolveDifferences : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Departments", "DepartmentCode", c => c.String(nullable: false));
            AlterColumn("dbo.Departments", "DepartmentName", c => c.String(nullable: false));
            AlterColumn("dbo.Departments", "DepartmentContactName", c => c.String(nullable: false));
            AlterColumn("dbo.Departments", "DepartmentPhone", c => c.String(nullable: false));
            AlterColumn("dbo.Departments", "DepartmentFax", c => c.String(nullable: false));
            AlterColumn("dbo.Departments", "DepartmentHeadName", c => c.String(nullable: false));
            AlterColumn("dbo.Departments", "DepartmentRepresentative", c => c.String(nullable: false));
            AlterColumn("dbo.Suppliers", "SupplierCode", c => c.String(nullable: false));
            AlterColumn("dbo.Suppliers", "SupplierName", c => c.String(nullable: false));
            AlterColumn("dbo.Suppliers", "SupplierContactName", c => c.String(nullable: false));
            AlterColumn("dbo.Suppliers", "SupplierPhone", c => c.String(nullable: false));
            AlterColumn("dbo.Suppliers", "SupplierFax", c => c.String(nullable: false));
            AlterColumn("dbo.Suppliers", "SupplierAddress", c => c.String(nullable: false));
            AlterColumn("dbo.Suppliers", "SupplierGSTNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Suppliers", "SupplierGSTNumber", c => c.String());
            AlterColumn("dbo.Suppliers", "SupplierAddress", c => c.String());
            AlterColumn("dbo.Suppliers", "SupplierFax", c => c.String());
            AlterColumn("dbo.Suppliers", "SupplierPhone", c => c.String());
            AlterColumn("dbo.Suppliers", "SupplierContactName", c => c.String());
            AlterColumn("dbo.Suppliers", "SupplierName", c => c.String());
            AlterColumn("dbo.Suppliers", "SupplierCode", c => c.String());
            AlterColumn("dbo.Departments", "DepartmentRepresentative", c => c.String());
            AlterColumn("dbo.Departments", "DepartmentHeadName", c => c.String());
            AlterColumn("dbo.Departments", "DepartmentFax", c => c.String());
            AlterColumn("dbo.Departments", "DepartmentPhone", c => c.String());
            AlterColumn("dbo.Departments", "DepartmentContactName", c => c.String());
            AlterColumn("dbo.Departments", "DepartmentName", c => c.String());
            AlterColumn("dbo.Departments", "DepartmentCode", c => c.String());
        }
    }
}
