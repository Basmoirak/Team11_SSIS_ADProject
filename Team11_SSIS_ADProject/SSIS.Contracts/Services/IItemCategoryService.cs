using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Contracts
{
    public interface IItemCategoryService
    {
        void Save(ItemCategory itemCategory);
        ItemCategory Get(string id);
        IEnumerable<ItemCategory> GetAll();
        IEnumerable<SelectListItem> GetCategoryList();
        void Delete(string Id);
    }
}