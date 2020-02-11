using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.SSIS.Contracts
{
    public interface IItemRequisitionService
    {
        void Save(ItemRequisition itemRequisition);
        ItemCategory Get(string id);
        IEnumerable<ItemRequisition> GetAll();
        void Delete(string Id);
        IEnumerable<ItemRequisition> GetAllByRequisitionId(string Id);
        IEnumerable<GroupedItemID> groupItemRequisitionsByDateRange(DateTime startDate, DateTime endDate);
    }
}
