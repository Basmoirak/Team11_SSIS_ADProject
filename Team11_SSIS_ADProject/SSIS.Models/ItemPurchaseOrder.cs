using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.Models
{
    public class ItemPurchaseOrder : BaseEntity
    {
        [Required]
        public string ItemId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
        public string Remark { get; set; }
        public Item Item { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
    }
}