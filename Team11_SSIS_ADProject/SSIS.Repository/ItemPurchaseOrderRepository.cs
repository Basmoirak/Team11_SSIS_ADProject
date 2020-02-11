using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts.Repositories;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.SSIS.Repository
{

    public class ItemPurchaseOrderRepository : Repository<ItemPurchaseOrder>, IItemPurchaseOrderRepository
    {
        public IEnumerable<GroupedItemID> groupItemPurchaseOrdersByDateRange(DateTime startDate, DateTime endDate)
        {
            DateTime newEndDate = endDate.AddDays(1).AddTicks(-1);

            var grouped = _context.PurchaseOrders
                            .Include("ItemPurchaseOrders")
                            .Include("Items")
                            .Where(x => x.createdDateTime >= startDate && x.createdDateTime <= newEndDate)
                            .SelectMany(x => x.ItemPurchaseOrders)
                            .Select(x => new GroupedItemID
                            {
                                ItemCode = "dd",
                                ItemDescription = "dd",
  
                            }).ToList();
            return grouped;
        }
    }
}