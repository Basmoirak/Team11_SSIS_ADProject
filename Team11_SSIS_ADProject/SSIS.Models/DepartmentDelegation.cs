using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.Models
{
    public class DepartmentDelegation : BaseEntity
    {

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public string DepartmentId { get; set; }
        public Department Department { get; set; }
        [Required]
        public string UserId { get; set; }
       
        public bool Status { get; set; }

    }
}