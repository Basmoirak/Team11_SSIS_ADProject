using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.Models
{
    public class Item : BaseEntity
    {
        [Required]
        public string ItemNumber { get; set; }
        [Required]
        public string ItemDescription { get; set; }
        [Required]
        public string ItemReorderLevel { get; set; }
        [Required]
        public string ItemReorderQty { get; set; }
        [Required]
        public string ItemUnit { get; set; }
        [Required]
        public string ItemCategoryId { get; set; }
        public virtual ItemCategory ItemCategory { get; set; }
        public string ImagePath { get; set; }


    }
}