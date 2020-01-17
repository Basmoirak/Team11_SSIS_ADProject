using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.ViewModels
{
    public class SupplierViewModel : BaseEntity
    {
     
        [Required]
        [Display(Name = "Supplier Code")]
        public string SupplierCode { get; set; }
        [Required]
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }
        [Required]
        [Display(Name = "Contact Name")]
        public string SupplierContactName { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string SupplierPhone { get; set; }
        [Required]
        [Display(Name = "Fax Number")]
        public string SupplierFax { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string SupplierAddress { get; set; }
        [Required]
        [Display(Name = "GST Registration No")]
        public string SupplierGSTNumber { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
    }
}