using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.Models
{
    public static class CustomRoles
    {
        public const string CanManageSupplier = "StoreClerk,StoreManager,StoreSupervisor";
        public const string CanManageItemCategory = "StoreClerk,StoreManager,StoreSupervisor";
        public const string CanManageDepartment = "Admin";
        public const string CanManageDepartmentDelegation = "DepartmentHead";
    }
}