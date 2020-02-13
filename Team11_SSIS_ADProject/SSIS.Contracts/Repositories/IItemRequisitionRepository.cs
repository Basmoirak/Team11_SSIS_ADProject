using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.SSIS.Contracts
{
    public interface IItemRequisitionRepository : IRepository<ItemRequisition>
    {
        IEnumerable<ItemRequisition> GetAllByRequisitionId(string id);
        IEnumerable<GroupedItemID>  groupItemRequisitionsByDateRange(DateTime startDate, DateTime endDate);
        IEnumerable<GroupedItemID> groupDepartmentItemRequisitionsByDateRange(DateTime startDate, DateTime endDate, string departmentId);
        IEnumerable<GroupedItemID> ItemRequisitionsThisWeek();
        IEnumerable<GroupedItemID> ItemRequisitionsTrend(string id);
    }
}
