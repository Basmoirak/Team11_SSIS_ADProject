namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropdepartmentfrompurchaseorder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PurchaseOrders", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.PurchaseOrders", new[] { "DepartmentId" });
            AlterColumn("dbo.PurchaseOrders", "DepartmentId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PurchaseOrders", "DepartmentId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.PurchaseOrders", "DepartmentId");
            AddForeignKey("dbo.PurchaseOrders", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
        }
    }
}
