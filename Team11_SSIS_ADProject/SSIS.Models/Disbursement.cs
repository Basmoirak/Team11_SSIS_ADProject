using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.Models
{
    public class Disbursement : BaseEntity
    {
        [Required]
        public string DepartmentId { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
        public virtual Department Department { get; set; }
        public ICollection<ItemDisbursement> ItemDisbursements { get; set; }

    }
}