using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.Models
{
    public class ItemSupplier : BaseEntity
    {
        [Required]
        public string ItemId { get; set; }
        [Required]
        public string SupplierId { get; set; }
        [Required]
        public int Priority { get; set; }
        [Required]
        public double Price { get; set; }
        public virtual Item Item { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}