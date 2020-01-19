namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateitem : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Items", name: "itemCategory_Id", newName: "ItemCategoryId");
            RenameIndex(table: "dbo.Items", name: "IX_itemCategory_Id", newName: "IX_ItemCategoryId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Items", name: "IX_ItemCategoryId", newName: "IX_itemCategory_Id");
            RenameColumn(table: "dbo.Items", name: "ItemCategoryId", newName: "itemCategory_Id");
        }
    }
}
