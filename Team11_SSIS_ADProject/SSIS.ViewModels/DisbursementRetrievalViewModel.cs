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

    public class DisbursementRetrievalMobileViewModel
    {
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

    public class CollectionsViewModel
    {
        public IEnumerable<GroupedDepartmentCollections> groupedDepartmentCollections { get; set; }
    }

    public class GroupedDepartmentCollections
    {
        public string DepartmentName { get; set; }
        public string CollectionPoint { get; set; }
        public IEnumerable<GroupedItemCollection> ItemDisbursements { get; set; }
    }

    public class GroupedItemCollection
    {
        public string ItemID { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public int RequestedQuantity { get; set; }
        public int AvailableQuantity { get; set; }
    }
}