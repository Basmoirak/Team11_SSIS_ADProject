using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Service
{
    public class ItemService : IItemService
    {
        IItemRepository itemContext;

        public ItemService(IItemRepository itemRepository)
        {
            this.itemContext = itemRepository;
        }

        public void Delete(string id)
        {
            var item = itemContext.Get(id);
            itemContext.Remove(item);
            itemContext.Commit();
        }

        public Item Get(string id)
        {
            return itemContext.Get(id);
        }

        public IEnumerable<Item> GetAll()
        {
            return itemContext.GetAll();
        }

        public void Save(Item item)
        {
            Item i = itemContext.Get(item.Id);
            if(i == null)
            {
                itemContext.Add(item);
            }
            else
            {
                i.ItemNumber = item.ItemNumber;
                i.ItemDescription = item.ItemDescription;
                i.ItemReorderLevel = item.ItemReorderLevel;
                i.ItemReorderQty = item.ItemReorderQty;
                i.ItemUnit = item.ItemUnit;
                i.ItemCategoryId = item.ItemCategoryId;
                i.ImagePath = item.ImagePath;
            }
            itemContext.Commit();
        }
    }
}