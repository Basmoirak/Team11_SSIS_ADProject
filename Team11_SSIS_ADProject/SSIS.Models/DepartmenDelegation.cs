using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.Models
{
    public class DepartmentDelegation : BaseEntity
    {

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        public string DepartmentId { get; set; }
        public Department Department { get; set; }

        [Required]
        public string UserId { get; set; }
       
        public string Status { get; set; }


    }
}