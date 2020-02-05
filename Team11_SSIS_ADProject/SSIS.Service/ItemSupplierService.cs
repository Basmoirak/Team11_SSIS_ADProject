using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;


namespace Team11_SSIS_ADProject.SSIS.Service
{
    public class ItemSupplierService : IItemSupplierService
    {
        IItemSupplierRepository itemSupplierContext;
        public ItemSupplierService(IItemSupplierRepository itemSupplierContext)
        {
            this.itemSupplierContext = itemSupplierContext;
        }

        public void Delete(string Id)
        {
            var i = itemSupplierContext.Get(Id);
            itemSupplierContext.Remove(i);
            itemSupplierContext.Commit();
        }

        public ItemSupplier Get(string id)
        {
            return itemSupplierContext.Get(id);
        }

        public IEnumerable<ItemSupplier> GetAll()
        {
            return itemSupplierContext.GetAll();
        }

        public IEnumerable<ItemSupplier> GetSuppliersByItem(string Id)
        {
            return itemSupplierContext.GetAll()
                                       .Where(s => s.ItemId == Id)
                                       .OrderBy(s=> s.Priority)
                                       .ToList();
        }

        public void Save(ItemSupplier itemSupplier)
        {
            int count = GetSuppliersByItem(itemSupplier.ItemId).Count();
            int p = 0;

            if(count == 0)
            {
                p = 3;
            }
            else if(count == 1)
            {
                p = 2;
            }
            else if(count == 2)
            {
                p = 1;
            }

                ItemSupplier iSup = new ItemSupplier()
                {
                    ItemId = itemSupplier.ItemId,
                    Price = itemSupplier.Price,
                    SupplierId = itemSupplier.SupplierId,
                    Priority = p
                };
            itemSupplierContext.Add(iSup);
            itemSupplierContext.Commit();
        }

        public void UpdatePriority(string id)
        {
            ItemSupplier itemSupplier = itemSupplierContext.Get(id);
            itemSupplier.Priority += 1;
            itemSupplierContext.Commit();
        }

        //overloading the updatePriority method
        public void UpdatePriority(string id, int order)
        {
            ItemSupplier itemSupplier = itemSupplierContext.Get(id);
            itemSupplier.Priority = order;
            itemSupplierContext.Commit();
        }
    }
}