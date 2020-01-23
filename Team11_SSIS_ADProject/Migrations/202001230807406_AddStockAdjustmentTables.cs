namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStockAdjustmentTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemStockAdjustments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ItemId = c.String(nullable: false, maxLength: 128),
                        StockAdjustmentId = c.String(nullable: false, maxLength: 128),
                        StockMovement = c.Int(nullable: false),
                        OldQuantity = c.Int(nullable: false),
                        NewQuantity = c.Int(nullable: false),
                        createdDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.StockAdjustments", t => t.StockAdjustmentId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.StockAdjustmentId);
            
            CreateTable(
                "dbo.StockAdjustments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Remarks = c.String(),
                        CreatedBy = c.String(),
                        ApprovedBy = c.String(),
                        Status = c.Int(nullable: false),
                        createdDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemStockAdjustments", "StockAdjustmentId", "dbo.StockAdjustments");
            DropForeignKey("dbo.ItemStockAdjustments", "ItemId", "dbo.Items");
            DropIndex("dbo.ItemStockAdjustments", new[] { "StockAdjustmentId" });
            DropIndex("dbo.ItemStockAdjustments", new[] { "ItemId" });
            DropTable("dbo.StockAdjustments");
            DropTable("dbo.ItemStockAdjustments");
        }
    }
}
