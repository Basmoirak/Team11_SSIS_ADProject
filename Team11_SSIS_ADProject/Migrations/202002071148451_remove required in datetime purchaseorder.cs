namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removerequiredindatetimepurchaseorder : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ItemPurchaseOrders", name: "PurchaseOrder_Id", newName: "PurchaseOrderId");
            RenameIndex(table: "dbo.ItemPurchaseOrders", name: "IX_PurchaseOrder_Id", newName: "IX_PurchaseOrderId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ItemPurchaseOrders", name: "IX_PurchaseOrderId", newName: "IX_PurchaseOrder_Id");
            RenameColumn(table: "dbo.ItemPurchaseOrders", name: "PurchaseOrderId", newName: "PurchaseOrder_Id");
        }
    }
}
