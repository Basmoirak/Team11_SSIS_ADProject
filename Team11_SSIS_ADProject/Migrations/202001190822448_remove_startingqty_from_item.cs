namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_startingqty_from_item : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Items", "ItemStartingQuantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "ItemStartingQuantity", c => c.String());
        }
    }
}
