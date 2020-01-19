using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.Repository;

namespace Team11_SSIS_ADProject.SSIS.Service
{
    public class ItemCategoryService : IItemCategoryService
    {
        IItemCategoryRepository itemCategoryContext;
        public ItemCategoryService(IItemCategoryRepository itemCategoryRepository)
        {
            this.itemCategoryContext = itemCategoryRepository;
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
        public IEnumerable<SelectListItem> GetCategoryList()
        {
            List<SelectListItem> categories = itemCategoryContext.GetAll()
                                              .OrderBy(c => c.Name)
                                              .Select(c =>
                                                new SelectListItem
                                                {
                                                    Value = c.Id,
                                                    Text = c.Name
                                                }).ToList();
            var placeholder = new SelectListItem()
            {
                Value = null,
                Text = "--Select Category--"
            };
            categories.Insert(0, placeholder);
            return new SelectList(categories, "Value", "Text");

        }

        public void Delete(string Id)
        {
            var itemCategory = itemCategoryContext.Get(Id);
            itemCategoryContext.Remove(itemCategory);
            itemCategoryContext.Commit();
        }
    }
}