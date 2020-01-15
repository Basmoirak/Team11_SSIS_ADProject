using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.Models
{
    public class Supplier : BaseEntity
    {
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string SupplierContactName { get; set; }
        public string SupplierPhone { get; set; }
        public string SupplierFax { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierGSTNumber { get; set; }
    }
}