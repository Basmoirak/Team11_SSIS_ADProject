using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Contracts.Services
{
    public interface IItemStockAdjustmentService
    {
        void Delete(string id);
        ItemStockAdjustment Get(string id);
        IEnumerable<ItemStockAdjustment> GetAll();
        void Save(ItemStockAdjustment stockAdjustment);
        IEnumerable<ItemStockAdjustment> FindByStockAdjustmentId(string id);
    }
}