using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.SSIS.Repository
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public IEnumerable<ItemPurchaseOrderViewModel> GetItemsLowerThanReorderLevel()
        {
            var result = _context.Items
                        .Include("ItemSuppliers")
                        .Where(x => x.Inventory.Quantity < x.ItemReorderLevel)
                        .Select(x => new ItemPurchaseOrderViewModel
                        {
                            ItemSuppliers = x.ItemSuppliers.OrderBy(p => p.Priority),
                            Price = x.ItemSuppliers.Where(s => s.Priority == 1).Select(s => s.Price).FirstOrDefault(), 
                            ItemId = x.Id,
                            ItemNumber = x.ItemNumber,
                            ItemDescription = x.ItemDescription,
                            InventoryQuantity = x.Inventory.Quantity,
                            ItemReorderQty = x.ItemReorderQty,
                            ItemUnit = x.ItemUnit
                        })
                        .ToList();
            return result;
        }
    }
}