using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.ViewModels
{
    public class DepartmentViewModel : BaseEntity
    {
        [Required]
        [Display(Name = "Department Code")]
        public string DepartmentCode { get; set; }
        [Required]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        [Required]
        [Display(Name = "Contact Name")]
        public string DepartmentContactName { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string DepartmentPhone { get; set; }
        [Required]
        [Display(Name = "Fax Number")]
        public string DepartmentFax { get; set; }
        [Required]
        [Display(Name = "Department Head Name")]
        public string DepartmentHeadName { get; set; }
        [Required]
        [Display(Name = "Department Representative")]
        public string DepartmentRepresentative { get; set; }
        public IEnumerable<Department> Departments { get; set; }
    }
}