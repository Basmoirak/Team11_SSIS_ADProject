using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.Models
{
    public class Supplier : BaseEntity
    {
        [Required]
        public string SupplierCode { get; set; }
        [Required]
        public string SupplierName { get; set; }
        [Required]
        public string SupplierContactName { get; set; }
        [Required]
        public string SupplierPhone { get; set; }
        [Required]
        public string SupplierFax { get; set; }
        [Required]
        public string SupplierAddress { get; set; }
        [Required]
        public string SupplierGSTNumber { get; set; }
    }
}