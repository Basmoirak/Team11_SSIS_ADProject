namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedItemSupplierTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemSuppliers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ItemId = c.String(nullable: false, maxLength: 128),
                        SupplierId = c.String(nullable: false, maxLength: 128),
                        Priority = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        createdDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.SupplierId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemSuppliers", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.ItemSuppliers", "ItemId", "dbo.Items");
            DropIndex("dbo.ItemSuppliers", new[] { "SupplierId" });
            DropIndex("dbo.ItemSuppliers", new[] { "ItemId" });
            DropTable("dbo.ItemSuppliers");
        }
    }
}
