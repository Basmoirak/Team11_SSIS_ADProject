using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.SSIS.Contracts
{
    public interface IItemService
    {
        void Save(Item item);
        Item Get(string id);
        IEnumerable<Item> GetAll();
        void Delete(string id);
        IEnumerable<SelectListItem> GetItemList();
        IEnumerable<ItemPurchaseOrderViewModel> GetItemsLowerThanReorderLevel();
    }
}
