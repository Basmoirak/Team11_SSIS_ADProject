using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Repository
{
    public class ItemStockAdjustmentRepository : Repository<ItemStockAdjustment>, IItemStockAdjustmentRepository
    {
        public IQueryable<ItemStockAdjustment> FindByStockAdjustmentId(string id)
        {
            return _context.ItemStockAdjustments.Where(x => x.StockAdjustmentId == id);
        }
    }
}