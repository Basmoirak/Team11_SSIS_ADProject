using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;

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


    }
}