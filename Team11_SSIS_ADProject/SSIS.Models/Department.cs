using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.Models
{
    public class Department : BaseEntity
    {
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentContactName { get; set; }
        public string DepartmentPhone { get; set; }
        public string DepartmentFax { get; set; }
        public string DepartmentHeadName { get; set; }
        public string DepartmentRepresentative { get; set; }
    }
}