namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Team11_SSIS_ADProject.Models;
    using Team11_SSIS_ADProject.SSIS.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Team11_SSIS_ADProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Team11_SSIS_ADProject.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //Add Suppliers
            context.Suppliers.AddOrUpdate(c => c.Id, new Supplier()
            {
                Id = "1", SupplierCode = "ALPA", SupplierName = "ALPHA Office Supplies", SupplierContactName = "Ms Irene Tan",
                SupplierAddress = "Blk 1128, Ang Mo Kio Industrial Park #02-1108 Ang Mo Kio Street 62 Singapore 622262",
                SupplierFax = "461 2238", SupplierPhone = "461 9928", SupplierGSTNumber = "MR-8500440-2"
            });
            context.Suppliers.AddOrUpdate(c => c.Id, new Supplier()
            {
                Id = "2", SupplierCode = "CHEP", SupplierName = "Cheap Stationer", SupplierContactName = "Mr Soh Kway Koh",
                SupplierAddress = "Blk 34, Clementi Road #07-02 Ban Ban Soh Building Singapore 110525",
                SupplierFax = "474 2434", SupplierPhone = "354 3234", SupplierGSTNumber = null
            });
            context.Suppliers.AddOrUpdate(c => c.Id, new Supplier()
            {
                Id = "3", SupplierCode = "BANE", SupplierName = "BANES Shop", SupplierContactName = "Mr Loh Ah Pek",
                SupplierAddress = "Alexandra Road #03-04 Banes Building Singapore 550315",
                SupplierFax = "479 2434", SupplierPhone = "478 1234", SupplierGSTNumber = "MR-8200420-2"
            });
            context.Suppliers.AddOrUpdate(c => c.Id, new Supplier()
            {
                Id = "4", SupplierCode = "OMEG", SupplierName = "OEMGA Stationery Supplier", SupplierContactName = "Mr Ronnie Ho",
                SupplierAddress = "Blk 11, Hillview Avenue #03-04, Singapore 679036",
                SupplierFax = "767 1234", SupplierPhone = "767 1233", SupplierGSTNumber = "MR-8555330-1"
            });

            //Add Item Categories
            context.ItemCategories.AddOrUpdate(c => c.Id, new ItemCategory() { Id = "1", Name = "Clip"});
            context.ItemCategories.AddOrUpdate(c => c.Id, new ItemCategory() { Id = "2", Name = "Envelope"});
            context.ItemCategories.AddOrUpdate(c => c.Id, new ItemCategory() { Id = "3", Name = "Erasers"});
            context.ItemCategories.AddOrUpdate(c => c.Id, new ItemCategory() { Id = "4", Name = "Exercise Book"});
            context.ItemCategories.AddOrUpdate(c => c.Id, new ItemCategory() { Id = "5", Name = "File"});
            context.ItemCategories.AddOrUpdate(c => c.Id, new ItemCategory() { Id = "6", Name = "Pad"});
            context.ItemCategories.AddOrUpdate(c => c.Id, new ItemCategory() { Id = "7", Name = "Paper"});
            context.ItemCategories.AddOrUpdate(c => c.Id, new ItemCategory() { Id = "8", Name = "Pens"});
            context.ItemCategories.AddOrUpdate(c => c.Id, new ItemCategory() { Id = "9", Name = "Puncher"});
            context.ItemCategories.AddOrUpdate(c => c.Id, new ItemCategory() { Id = "10", Name = "Ruler"});
            context.ItemCategories.AddOrUpdate(c => c.Id, new ItemCategory() { Id = "11", Name = "Sharpener"});
            context.ItemCategories.AddOrUpdate(c => c.Id, new ItemCategory() { Id = "12", Name = "Scissors"});
            context.ItemCategories.AddOrUpdate(c => c.Id, new ItemCategory() { Id = "13", Name = "Shorthand"});
            context.ItemCategories.AddOrUpdate(c => c.Id, new ItemCategory() { Id = "14", Name = "Stapler"});
            context.ItemCategories.AddOrUpdate(c => c.Id, new ItemCategory() { Id = "15", Name = "Tack"});
            context.ItemCategories.AddOrUpdate(c => c.Id, new ItemCategory() { Id = "16", Name = "Tape"});
            context.ItemCategories.AddOrUpdate(c => c.Id, new ItemCategory() { Id = "17", Name = "Tparency"});
            context.ItemCategories.AddOrUpdate(c => c.Id, new ItemCategory() { Id = "18", Name = "Tray"});

            // Add Departments
            context.Departments.AddOrUpdate(c => c.Id, new Department()
            {
                Id = "1", DepartmentCode = "ENGL", DepartmentName = "English Department", DepartmentContactName = "Mrs Pamela Kow",
                DepartmentRepresentative = "4", DepartmentHeadName = "5", DepartmentFax = "879-555-666", DepartmentPhone = "8112-8383",
                DepartmentCollectionPoint = "Stationery Store - Administration Building"
            });
            context.Departments.AddOrUpdate(c => c.Id, new Department()
            {
                Id = "2", DepartmentCode = "COMP", DepartmentName = "Computer Science", DepartmentContactName = "Jane Doe",
                DepartmentRepresentative = "6", DepartmentHeadName = "7", DepartmentFax = "879-555-666", DepartmentPhone = "8112-8383",
                DepartmentCollectionPoint = "Science School"
            });
            context.Departments.AddOrUpdate(c => c.Id, new Department()
            {
                Id = "3", DepartmentCode = "ECON", DepartmentName = "Economics Department", DepartmentContactName = "Jane Doe",
                DepartmentRepresentative = "8", DepartmentHeadName = "9", DepartmentFax = "879-555-666", DepartmentPhone = "8112-8383",
                DepartmentCollectionPoint = "Management School"
            });
        }
    }
}
