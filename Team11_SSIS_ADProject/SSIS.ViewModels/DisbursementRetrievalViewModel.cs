using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.ViewModels
{
    public class DisbursementRetrievalViewModel
    {
        public IEnumerable<Disbursement> Disbursements { get; set; }
        public IEnumerable<GroupedItemID> GroupedItemDisbursements { get; set; }
    }

    public class GroupedItemID
    {
        public string ItemID { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public int RequestedQuantity { get; set; }
        public int AvailableQuantity { get; set; }
    }

    public class GroupedItemDisbursements
    {
        public string DepartmentName { get; set; }
        public IEnumerable<ItemDisbursement> ItemDisbursements { get; set; }
    }
}