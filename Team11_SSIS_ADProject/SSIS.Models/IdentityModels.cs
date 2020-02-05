using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Team11_SSIS_ADProject.SSIS.Models;
using System.Collections.Generic;

namespace Team11_SSIS_ADProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("DepartmentId", this.DepartmentId.ToString()));
            return userIdentity;
        }

        public string DepartmentId { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Requisition> Requisitions { get; set; }
        public DbSet<DepartmentDelegation> DepartmentDelegations { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ItemRequisition> ItemRequisitions { get; set; } 
        public DbSet<StockAdjustment> StockAdjustments { get; set; }
        public DbSet<ItemStockAdjustment> ItemStockAdjustments { get; set; }
        public DbSet<Disbursement> Disbursements { get; set; }
        public DbSet<ItemDisbursement> ItemDisbursements { get; set; }
        public DbSet<ItemSupplier> ItemSuppliers { get; set; }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<ItemPurchaseOrder> ItemPurchaseOrders { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}