using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Team11_SSIS_ADProject.SSIS.Models
{
    public class Notification : BaseEntity
    {
        [Required]
        public String To { get; set; }
        [Required]
        public String From { get; set; }
        [Required]
        public String Subject { get; set; }
        [Required]
        public String Body { get; set; }

    }

}