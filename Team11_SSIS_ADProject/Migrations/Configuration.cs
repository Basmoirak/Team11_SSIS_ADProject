namespace Team11_SSIS_ADProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
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
        }
    }
}
