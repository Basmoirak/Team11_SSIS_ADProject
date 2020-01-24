using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.Models
{
    public class StockAdjustment : BaseEntity
    {
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public int Status { get; set; }
        public ICollection<ItemStockAdjustment> ItemStockAdjustments { get; set; }
    }
}