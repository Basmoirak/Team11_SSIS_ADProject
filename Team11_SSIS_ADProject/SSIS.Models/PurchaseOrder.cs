using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.Models
{
    public class PurchaseOrder : BaseEntity
    {
        [Required]
        public string SupplierId { get; set; }
        public DateTime ExpectedDate { get; set; }
        [Required]
        public int Status { get; set; }
        public virtual Supplier Supplier { get; set; }
        public string Remark { get; set; }
        public IEnumerable<ItemPurchaseOrder> ItemPurchaseOrders { get; set; }
    }
}