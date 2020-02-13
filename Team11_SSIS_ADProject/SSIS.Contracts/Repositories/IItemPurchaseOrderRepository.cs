using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.SSIS.Contracts.Repositories
{
    public interface IItemPurchaseOrderRepository : IRepository<ItemPurchaseOrder>
    {
        IEnumerable<GroupedItemID> groupItemPurchaseOrdersByDateRange(DateTime startDate, DateTime endDate);
        IEnumerable<GroupedItemID> ItemPurchaseOrdersThisWeek();
    }
}
