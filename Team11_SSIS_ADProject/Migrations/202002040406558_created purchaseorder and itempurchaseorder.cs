namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdpurchaseorderanditempurchaseorder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemPurchaseOrders",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ItemId = c.String(nullable: false, maxLength: 128),
                        Quantity = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Remark = c.String(),
                        createdDateTime = c.DateTime(nullable: false),
                        PurchaseOrder_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrder_Id)
                .Index(t => t.ItemId)
                .Index(t => t.PurchaseOrder_Id);
            
            CreateTable(
                "dbo.PurchaseOrders",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DepartmentId = c.String(nullable: false, maxLength: 128),
                        SupplierId = c.String(nullable: false, maxLength: 128),
                        ExpectedDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Remark = c.String(),
                        createdDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.SupplierId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemPurchaseOrders", "PurchaseOrder_Id", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseOrders", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.PurchaseOrders", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.ItemPurchaseOrders", "ItemId", "dbo.Items");
            DropIndex("dbo.PurchaseOrders", new[] { "SupplierId" });
            DropIndex("dbo.PurchaseOrders", new[] { "DepartmentId" });
            DropIndex("dbo.ItemPurchaseOrders", new[] { "PurchaseOrder_Id" });
            DropIndex("dbo.ItemPurchaseOrders", new[] { "ItemId" });
            DropTable("dbo.PurchaseOrders");
            DropTable("dbo.ItemPurchaseOrders");
        }
    }
}
