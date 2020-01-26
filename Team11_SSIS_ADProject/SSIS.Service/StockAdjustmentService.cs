using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Service
{
    public class StockAdjustmentService : IStockAdjustmentService
    {
        IStockAdjustmentRepository stockAdjustmentContext;
        IItemStockAdjustmentRepository itemStockAdjustmentContext;
        IInventoryRepository inventoryContext;

        public StockAdjustmentService(IStockAdjustmentRepository stockAdjustmentRepository, 
            IItemStockAdjustmentRepository itemStockAdjustmentRepository, IInventoryRepository inventoryRepository)
        {
            this.stockAdjustmentContext = stockAdjustmentRepository;
            this.itemStockAdjustmentContext = itemStockAdjustmentRepository;
            this.inventoryContext = inventoryRepository;
        }

        public void Cancel(string Id)
        {
            var stockAdjustment = stockAdjustmentContext.Get(Id);
            stockAdjustment.Status = CustomStatus.Cancelled;
            stockAdjustmentContext.Commit();
        }

        public StockAdjustment Get(string id)
        {
            return stockAdjustmentContext.Get(id);
        }

        public IEnumerable<StockAdjustment> GetAll()
        {
            return stockAdjustmentContext.GetAll();
        }

        public void Save(StockAdjustment stockAdjustment)
        {
            StockAdjustment item = stockAdjustmentContext.Get(stockAdjustment.Id);
            if(item == null)
            {
                stockAdjustment.Status = CustomStatus.PendingApproval;
                stockAdjustmentContext.Add(stockAdjustment);
            }
            else
            {
                item.Remarks = stockAdjustment.Remarks;
            }
            stockAdjustmentContext.Commit();
        }

        public int GetItemQuantity(string id)
        {
            return inventoryContext.Get(id).Quantity;
        }

        public void UpdateInventory(string id, int stockMovement)
        {
            var inventory = inventoryContext.Get(id);
            inventory.Quantity += stockMovement;
            inventoryContext.Commit();
        }
    }
}