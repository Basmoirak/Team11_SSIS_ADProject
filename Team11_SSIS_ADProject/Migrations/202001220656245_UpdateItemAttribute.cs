namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateItemAttribute : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "ItemReorderLevel", c => c.Int(nullable: false));
            AlterColumn("dbo.Items", "ItemReorderQty", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Items", "ItemReorderQty", c => c.String(nullable: false));
            AlterColumn("dbo.Items", "ItemReorderLevel", c => c.String(nullable: false));
        }
    }
}
