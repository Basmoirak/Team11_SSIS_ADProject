using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Contracts.Repositories;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.SSIS.Service
{
    public class ChartService : IChartService
    {
        IInventoryRepository inventoryContext;
        IItemPurchaseOrderRepository itemPurchaseOrderContext;
        IItemRepository itemContext;
        IItemRequisitionRepository itemRequisitionContext;
        public ChartService(IItemPurchaseOrderRepository itemPurchaseOrderContext, IInventoryRepository inventoryContext, IItemRepository itemContext, IItemRequisitionRepository itemRequisitionContext)
        {
            this.itemContext = itemContext;
            this.inventoryContext = inventoryContext;
            this.itemPurchaseOrderContext = itemPurchaseOrderContext;
            this.itemRequisitionContext = itemRequisitionContext;
        }

        public IEnumerable<ItemViewModel> GetInventories()
        {
            return itemContext.GetInventories();
        }

        public IEnumerable<GroupedItemID> ItemPurchaseOrdersThisWeek()
        {
            return itemPurchaseOrderContext.ItemPurchaseOrdersThisWeek();
        }

        public IEnumerable<GroupedItemID> ItemRequisitionsThisWeek()
        {
            return itemRequisitionContext.ItemRequisitionsThisWeek();
        }

        public IEnumerable<GroupedItemID> ItemRequisitionsTrend(string id)
        {
            return itemRequisitionContext.ItemRequisitionsTrend(id);
        }
    }
}