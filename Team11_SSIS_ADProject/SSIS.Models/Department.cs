using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.Models
{
    public class Department : BaseEntity
    {
        [Required]
        public string DepartmentCode { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public string DepartmentContactName { get; set; }
        [Required]
        public string DepartmentPhone { get; set; }
        [Required]
        public string DepartmentFax { get; set; }
        [Required]
        public string DepartmentHeadName { get; set; }
        [Required]
        public string DepartmentRepresentative { get; set; }
    }
}