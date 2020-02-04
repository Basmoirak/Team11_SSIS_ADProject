namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropdepartmentIdfrompurchaseorder : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PurchaseOrders", "DepartmentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseOrders", "DepartmentId", c => c.String(nullable: false));
        }
    }
}
