using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Contracts
{
    public interface IItemSupplierService
    {
        void Save(ItemSupplier itemSupplier);
        ItemSupplier Get(string id);
        IEnumerable<ItemSupplier> GetAll();
        void Delete(string Id);
        IEnumerable<ItemSupplier> GetSuppliersByItem(string Id);
        void UpdatePriority(string id);
        void UpdatePriority(string id, int order);
        double GetItemPriceBySupplierIdAndItemId(string itemId, string supplierId);
    }
}
