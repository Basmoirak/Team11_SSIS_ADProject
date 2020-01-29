using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.Models
{
    public static class CustomRoles
    {
        // Admin Roles
        public const string CanManageDepartment = "Admin";

        //Department Roles
        public const string CanManageDepartmentDelegation = "DepartmentHead";
        public const string CanManageRequisitions = "DepartmentHead, Employee";

        //Store Roles
        public const string CanManageSupplier = "StoreClerk,StoreManager,StoreSupervisor";
        public const string CanManageItemCategory = "StoreClerk,StoreManager,StoreSupervisor";
        public const string CanManageItems = "StoreClerk,StoreManager,StoreSupervisor";
        public const string CanManageDisbursements = "StoreClerk,StoreManager,StoreSupervisor";
        public const string CanManageStockAdjustment = "StoreClerk,StoreManager,StoreSupervisor";

    }
}