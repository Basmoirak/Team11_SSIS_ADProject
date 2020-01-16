namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResolveDifferences1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Suppliers", "SupplierCode", c => c.String());
            AlterColumn("dbo.Suppliers", "SupplierName", c => c.String());
            AlterColumn("dbo.Suppliers", "SupplierContactName", c => c.String());
            AlterColumn("dbo.Suppliers", "SupplierPhone", c => c.String());
            AlterColumn("dbo.Suppliers", "SupplierFax", c => c.String());
            AlterColumn("dbo.Suppliers", "SupplierAddress", c => c.String());
            AlterColumn("dbo.Suppliers", "SupplierGSTNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Suppliers", "SupplierGSTNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Suppliers", "SupplierAddress", c => c.String(nullable: false));
            AlterColumn("dbo.Suppliers", "SupplierFax", c => c.String(nullable: false));
            AlterColumn("dbo.Suppliers", "SupplierPhone", c => c.String(nullable: false));
            AlterColumn("dbo.Suppliers", "SupplierContactName", c => c.String(nullable: false));
            AlterColumn("dbo.Suppliers", "SupplierName", c => c.String(nullable: false));
            AlterColumn("dbo.Suppliers", "SupplierCode", c => c.String(nullable: false));
        }
    }
}
