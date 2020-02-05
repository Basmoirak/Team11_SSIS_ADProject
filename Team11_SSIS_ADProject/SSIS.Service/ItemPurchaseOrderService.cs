using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts.Repositories;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Service
{
    public class ItemPurchaseOrderService : IItemPurchaseOrderService
    {
        IItemPurchaseOrderRepository itemPurchaseOrderContext;
        public ItemPurchaseOrderService(IItemPurchaseOrderRepository itemPurchaseOrderContext)
        {
            this.itemPurchaseOrderContext = itemPurchaseOrderContext;
        }
        public void Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public ItemPurchaseOrder Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemPurchaseOrder> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save(ItemPurchaseOrder itemPurchaseOrder)
        {
            itemPurchaseOrderContext.Add(itemPurchaseOrder);
            itemPurchaseOrderContext.Commit();
        }
    }
}