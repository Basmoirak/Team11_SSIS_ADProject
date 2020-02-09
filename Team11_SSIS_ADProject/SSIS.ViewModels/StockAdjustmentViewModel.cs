using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.ViewModels
{
    public class StockAdjustmentViewModel : BaseEntity
    {
        //Form View
        [Required]
        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string ItemDescription { get; set; }

        [Required]
        [Display(Name = "In Stock")]
        public int InStock { get; set; }

        [Required]
        public int Adjustment { get; set; }
        public int Status { get; set; }
        public IEnumerable<Item> Items { get; set; }

        // Index View
        [Required]
        public string Remarks { get; set; }
        public IEnumerable<StockAdjustment> StockAdjustments { get; set; }
        public IEnumerable<ItemStockAdjustment> itemStockAdjustments { get; set; }
    }

    public class MobileStockAdjustment
    {
        public string StockAdjustmentId { get; set; }
        public string ItemId { get; set; }
        public string CreatedBy { get; set; }
        public string Remarks { get; set; }
        public int Movement { get; set; }
    }

}