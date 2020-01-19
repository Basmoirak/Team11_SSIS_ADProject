namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ItemNumber = c.String(),
                        ItemDescription = c.String(),
                        ItemStartingQuantity = c.String(),
                        ItemReorderLevel = c.String(),
                        ItemReorderQty = c.String(),
                        ItemUnit = c.String(),
                        createdDateTime = c.DateTime(nullable: false),
                        itemCategory_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ItemCategories", t => t.itemCategory_Id)
                .Index(t => t.itemCategory_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "itemCategory_Id", "dbo.ItemCategories");
            DropIndex("dbo.Items", new[] { "itemCategory_Id" });
            DropTable("dbo.Items");
        }
    }
}
