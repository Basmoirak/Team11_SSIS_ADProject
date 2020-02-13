using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.SSIS.Contracts.Repositories
{
    public interface IItemDisbursementRepository : IRepository<ItemDisbursement>
    {
        IEnumerable<GroupedDepartmentCollections> groupItemDisbursementByDepartment();
        IEnumerable<MobileGroupedDepartmentCollections> groupItemDisbursementByDepartmentMobile();
        IEnumerable<GroupedItemID> groupItemDisbursementByItemID();
        IEnumerable<GroupedDepartmentCollections> GetDepartmentCollection(string departmentId);
        IEnumerable<GroupedDepartmentCollections> GetDepartmentCollectionForStore(string departmentId);
        IEnumerable<ItemDisbursement> getAllByDisbursementId(string id);
        IEnumerable<GroupedItemID> groupItemDisbursementsByDateRange(DateTime startDate, DateTime endDate);
    }
}
