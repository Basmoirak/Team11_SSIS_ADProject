using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.Models
{
    public class Inventory : BaseEntity
    {
        [Required]
        public int Quantity { get; set; }
    }
}