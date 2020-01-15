using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.Models
{
    public class ItemCategory : BaseEntity
    {
        [Required]
        public string Name { get; set; }

    }
}