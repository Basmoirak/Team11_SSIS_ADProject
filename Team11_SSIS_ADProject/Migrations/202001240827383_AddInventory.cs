namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInventory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Quantity = c.Int(nullable: false),
                        createdDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Items", "InventoryId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Items", "InventoryId");
            AddForeignKey("dbo.Items", "InventoryId", "dbo.Inventories", "Id");
            DropColumn("dbo.Items", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "Quantity", c => c.Int(nullable: false));
            DropForeignKey("dbo.Items", "InventoryId", "dbo.Inventories");
            DropIndex("dbo.Items", new[] { "InventoryId" });
            DropColumn("dbo.Items", "InventoryId");
            DropTable("dbo.Inventories");
        }
    }
}
