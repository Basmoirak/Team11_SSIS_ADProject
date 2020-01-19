namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_required_to_item_attributes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "ItemCategoryId", "dbo.ItemCategories");
            DropIndex("dbo.Items", new[] { "ItemCategoryId" });
            AlterColumn("dbo.Items", "ItemNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Items", "ItemDescription", c => c.String(nullable: false));
            AlterColumn("dbo.Items", "ItemReorderLevel", c => c.String(nullable: false));
            AlterColumn("dbo.Items", "ItemReorderQty", c => c.String(nullable: false));
            AlterColumn("dbo.Items", "ItemUnit", c => c.String(nullable: false));
            AlterColumn("dbo.Items", "ItemCategoryId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Items", "ItemCategoryId");
            AddForeignKey("dbo.Items", "ItemCategoryId", "dbo.ItemCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "ItemCategoryId", "dbo.ItemCategories");
            DropIndex("dbo.Items", new[] { "ItemCategoryId" });
            AlterColumn("dbo.Items", "ItemCategoryId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Items", "ItemUnit", c => c.String());
            AlterColumn("dbo.Items", "ItemReorderQty", c => c.String());
            AlterColumn("dbo.Items", "ItemReorderLevel", c => c.String());
            AlterColumn("dbo.Items", "ItemDescription", c => c.String());
            AlterColumn("dbo.Items", "ItemNumber", c => c.String());
            CreateIndex("dbo.Items", "ItemCategoryId");
            AddForeignKey("dbo.Items", "ItemCategoryId", "dbo.ItemCategories", "Id");
        }
    }
}
