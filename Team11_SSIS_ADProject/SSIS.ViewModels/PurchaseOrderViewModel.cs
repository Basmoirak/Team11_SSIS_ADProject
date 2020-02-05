using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.ViewModels
{
    public class PurchaseOrderViewModel : BaseEntity
    {
        [Required]
        public string SupplierId { get; set; }
        [Required]
        public DateTime ExpectedDate { get; set; }    
        public int Status { get; set; }
        public Supplier Supplier { get; set; }
        public string Remark { get; set; }
        public IEnumerable<ItemPurchaseOrder> ItemPurchaseOrders { get; set; }
        public IEnumerable<PurchaseOrder> PurchaseOrders { get; set; }
        public IEnumerable<ItemSupplier> ItemSuppliers { get; set; }
    }
}