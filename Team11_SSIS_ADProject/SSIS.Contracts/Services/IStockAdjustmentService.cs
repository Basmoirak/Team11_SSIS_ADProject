using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Contracts.Services
{
    public interface IStockAdjustmentService
    {
        void Cancel(string id);
        StockAdjustment Get(string id);
        IEnumerable<StockAdjustment> GetAll();
        void Save(StockAdjustment stockAdjustment);
        int GetItemQuantity(string id);
        void UpdateInventory(string id, int stockMovement);
    }
}
