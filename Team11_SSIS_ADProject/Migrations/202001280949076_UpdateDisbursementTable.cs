namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDisbursementTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemDisbursements", "RequestedQuantity", c => c.Int(nullable: false));
            AddColumn("dbo.ItemDisbursements", "AvailableQuantity", c => c.Int(nullable: false));
            AddColumn("dbo.ItemDisbursements", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.ItemDisbursements", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemDisbursements", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.ItemDisbursements", "Status");
            DropColumn("dbo.ItemDisbursements", "AvailableQuantity");
            DropColumn("dbo.ItemDisbursements", "RequestedQuantity");
        }
    }
}
