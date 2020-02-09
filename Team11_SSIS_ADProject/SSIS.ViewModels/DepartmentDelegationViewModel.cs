using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.Models;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.ViewModels
{
    public class DepartmentDelegationViewModel : BaseEntity
    {

        [Required]
        [Display(Name ="Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public string DepartmentId { get; set; }
        public Department Department { get; set; }
        [Required]
        [Display(Name = "Select User to assign")]
        public string UserId { get; set; }
        public bool Status { get; set; }
        public IEnumerable<DepartmentDelegation> DepartmentDelegations { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
       
    }
}