namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDisbursementAndItemDisbursementTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Disbursements",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DepartmentId = c.String(nullable: false, maxLength: 128),
                        Remarks = c.String(),
                        Status = c.Int(nullable: false),
                        createdDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.ItemDisbursements",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DisbursementId = c.String(nullable: false, maxLength: 128),
                        ItemId = c.String(nullable: false, maxLength: 128),
                        Quantity = c.Int(nullable: false),
                        Remark = c.String(),
                        createdDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Disbursements", t => t.DisbursementId, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.DisbursementId)
                .Index(t => t.ItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemDisbursements", "ItemId", "dbo.Items");
            DropForeignKey("dbo.ItemDisbursements", "DisbursementId", "dbo.Disbursements");
            DropForeignKey("dbo.Disbursements", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.ItemDisbursements", new[] { "ItemId" });
            DropIndex("dbo.ItemDisbursements", new[] { "DisbursementId" });
            DropIndex("dbo.Disbursements", new[] { "DepartmentId" });
            DropTable("dbo.ItemDisbursements");
            DropTable("dbo.Disbursements");
        }
    }
}
