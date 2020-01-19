using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.Models
{
    public class ItemRequisition : BaseEntity
    {
        [Required]
        public string RequisitionId { get; set; }
        [Required]
        public string ItemId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string Remark { get; set; }
        public virtual Requisition Requisition { get; set; }
        public virtual Item Item { get; set; }
    }
}