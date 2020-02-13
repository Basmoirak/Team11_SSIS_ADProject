using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.SSIS.Contracts.Services
{
    public interface IChartService
    {
        IEnumerable<ItemViewModel> GetInventories();
        IEnumerable<GroupedItemID> ItemRequisitionsThisWeek();
        IEnumerable<GroupedItemID> ItemPurchaseOrdersThisWeek();
        IEnumerable<GroupedItemID> ItemRequisitionsTrend(string id);
    }
}
