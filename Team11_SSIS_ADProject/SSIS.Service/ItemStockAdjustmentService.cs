using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Service
{
    public class ItemStockAdjustmentService : IItemStockAdjustmentService
    {
        IStockAdjustmentRepository stockAdjustmentContext;
        IItemStockAdjustmentRepository itemStockAdjustmentContext;
        IInventoryRepository inventoryContext;

        public ItemStockAdjustmentService(IStockAdjustmentRepository stockAdjustmentRepository,
            IItemStockAdjustmentRepository itemStockAdjustmentRepository, IInventoryRepository inventoryRepository)
        {
            this.stockAdjustmentContext = stockAdjustmentRepository;
            this.itemStockAdjustmentContext = itemStockAdjustmentRepository;
            this.inventoryContext = inventoryRepository;
        }

        public void Delete(string Id)
        {
            var itemStockAdjustment = itemStockAdjustmentContext.Get(Id);
            itemStockAdjustmentContext.Remove(itemStockAdjustment);
            itemStockAdjustmentContext.Commit();
        }

        public IEnumerable<ItemStockAdjustment> FindByStockAdjustmentId(string id)
        {
            return itemStockAdjustmentContext.FindByStockAdjustmentId(id);
        }

        public ItemStockAdjustment Get(string id)
        {
            return itemStockAdjustmentContext.Get(id);
        }

        public IEnumerable<ItemStockAdjustment> GetAll()
        {
            return itemStockAdjustmentContext.GetAll();
        }

        public void Save(ItemStockAdjustment itemStockAdjustment)
        {
            ItemStockAdjustment item = itemStockAdjustmentContext.Get(itemStockAdjustment.Id);
            if(item == null)
            {
                itemStockAdjustmentContext.Add(itemStockAdjustment);
            }
            else
            {
                item.OldQuantity = itemStockAdjustment.OldQuantity;
                item.NewQuantity = itemStockAdjustment.NewQuantity;
                item.StockMovement = itemStockAdjustment.StockMovement;
            }
            itemStockAdjustmentContext.Commit();
        }
        
    }
}