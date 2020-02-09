using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.ViewModels
{
    public class PurchaseOrderViewModel : BaseEntity
    {
        public string SupplierId { get; set; }
        public DateTime ExpectedDate { get; set; }
        public int Status { get; set; }
        public Supplier Supplier { get; set; }
        public string Remark { get; set; }
        public IEnumerable<ItemPurchaseOrder> ItemPurchaseOrders { get; set; }
        public IEnumerable<ItemPurchaseOrderViewModel> ItemPurchaseOrderViewModels { get; set; }
        public IEnumerable<PurchaseOrder> PurchaseOrders { get; set; }
        public IEnumerable<ItemSupplier> ItemSuppliersByItemId { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public int PendingOrderCount { get; set; }
        
    }

    public class ItemPurchaseOrderViewModel
    {
        public IEnumerable<ItemSupplier> ItemSuppliers { get; set; }
        public string ItemId { get; set; }
        public string ItemNumber { get; set; }
        public string ItemDescription { get; set; }
        public int InventoryQuantity { get; set; }
        public int ItemReorderQty { get; set; }
        public string ItemUnit { get; set; }
        public double Price { get; set; }
        public string SupplierId { get; set; }

    }
}