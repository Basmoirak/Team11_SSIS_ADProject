namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addimagepathtoitems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "ImagePath");
        }
    }
}
