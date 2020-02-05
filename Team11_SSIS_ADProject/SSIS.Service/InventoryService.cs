using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Service
{
    public class InventoryService : IInventoryService
    {
        IInventoryRepository inventoryContext;
        public InventoryService(IInventoryRepository inventoryRepository)
        {
            this.inventoryContext = inventoryRepository;
        }
        public Inventory Get(string id)
        {
            return inventoryContext.Get(id);
        }

        public IEnumerable<Inventory> GetAll()
        {
            return inventoryContext.GetAll();
        }

        public void Update(Inventory inventory)
        {
            //Retrieve inventory item
            var inventoryInDb = inventoryContext.Get(inventory.Id);

            //If in DB, update
            if (inventoryInDb != null)
                inventoryInDb.Quantity = inventory.Quantity;

            //Commit changes
            inventoryContext.Commit();
        }
    }
}