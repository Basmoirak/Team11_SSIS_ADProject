using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.Models
{
    public class ItemStockAdjustment : BaseEntity
    {
        [Required]
        public string ItemId { get; set; }
        [Required]
        public string StockAdjustmentId { get; set; }
        [Required]
        public int StockMovement { get; set; }
        [Required]
        public int OldQuantity { get; set; }
        [Required]
        public int NewQuantity { get; set; }
        public virtual Item Item { get; set; }
        public virtual StockAdjustment StockAdjustment { get; set; }
    }
}