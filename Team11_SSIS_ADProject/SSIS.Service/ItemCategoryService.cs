using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.Repository;

namespace Team11_SSIS_ADProject.SSIS.Service
{
    public class ItemCategoryService : IItemCategoryService
    {
        ItemCategoryRepository itemCategoryContext;
        public ItemCategoryService()
        {
            this.itemCategoryContext = new ItemCategoryRepository();
        }
        public void Save(ItemCategory itemCategory)
        {
            //Check if itemCategory is in DB
            var itemCategoryInDb = itemCategoryContext.Get(itemCategory.Id);

            //If not in DB, create new
            if(itemCategoryInDb == null)
                itemCategoryContext.Add(itemCategory);
            //If in DB, update
            else
            {
                itemCategoryInDb.Name = itemCategory.Name;
            }

            //Commit changes
            itemCategoryContext.Commit();
        }

        public ItemCategory Get(string id)
        {
            return itemCategoryContext.Get(id);
        }

        public IEnumerable<ItemCategory> GetAll()
        {
            return itemCategoryContext.GetAll();
        }

        public void Delete(string Id)
        {
            var itemCategory = itemCategoryContext.Get(Id);
            itemCategoryContext.Remove(itemCategory);
        }
    }
}