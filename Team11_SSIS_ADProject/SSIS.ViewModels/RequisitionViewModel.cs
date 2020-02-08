using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.ViewModels
{
    public class RequisitionViewModel : BaseEntity
    {
        [Display(Name = "Choose Item")]
        public string ItemId { get; set; }
        public string DepartmentId { get; set; }
        [Display(Name = "Remark")]
        public string Remark { get; set; }
        public int Status { get; set; }
        public virtual Department Department { get; set; }
        public IEnumerable<Requisition> Requisitions { get; set; }
        public IEnumerable<ItemRequisition> ItemRequisitions { get; set; }
        public IEnumerable<Item> Items { get; set; }
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
    }

    public class RequisitionMobileViewModel
    {
        public string DepartmentId { get; set; }
        public string RequisitionId { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
        public List<RequisitionDetailsMobileViewModel> ItemRequisitions { get; set; }
    }

    public class RequisitionDetailsMobileViewModel
    {
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}