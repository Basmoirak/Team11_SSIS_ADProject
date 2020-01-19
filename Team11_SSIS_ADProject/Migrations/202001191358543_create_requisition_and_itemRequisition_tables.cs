namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_requisition_and_itemRequisition_tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemRequisitions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        RequisitionId = c.String(nullable: false, maxLength: 128),
                        ItemId = c.String(nullable: false, maxLength: 128),
                        Quantity = c.Int(nullable: false),
                        Remark = c.String(),
                        createdDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.Requisitions", t => t.RequisitionId, cascadeDelete: true)
                .Index(t => t.RequisitionId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Requisitions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DepartmentId = c.String(nullable: false, maxLength: 128),
                        Remark = c.String(),
                        Status = c.Int(nullable: false),
                        createdDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemRequisitions", "RequisitionId", "dbo.Requisitions");
            DropForeignKey("dbo.Requisitions", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.ItemRequisitions", "ItemId", "dbo.Items");
            DropIndex("dbo.Requisitions", new[] { "DepartmentId" });
            DropIndex("dbo.ItemRequisitions", new[] { "ItemId" });
            DropIndex("dbo.ItemRequisitions", new[] { "RequisitionId" });
            DropTable("dbo.Requisitions");
            DropTable("dbo.ItemRequisitions");
        }
    }
}
