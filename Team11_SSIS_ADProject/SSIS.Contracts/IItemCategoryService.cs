using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Contracts
{
    public interface IItemCategoryService
    {
        void Save(ItemCategory itemCategory);
        ItemCategory Get(string id);
        IEnumerable<ItemCategory> GetAll();
        void Delete(string Id);
    }
}