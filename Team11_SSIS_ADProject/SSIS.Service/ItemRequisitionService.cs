using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.SSIS.Service
{
    public class ItemRequisitionService : IItemRequisitionService
    {
        IItemRequisitionRepository itemRequisitionContext;
        public ItemRequisitionService(IItemRequisitionRepository itemRequisitionContext)
        {
            this.itemRequisitionContext = itemRequisitionContext;
        }
        public void Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public ItemCategory Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemRequisition> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemRequisition> GetAllByRequisitionId(string Id)
        {
            return itemRequisitionContext.GetAllByRequisitionId(Id);
        }

        public IEnumerable<GroupedItemID> groupItemRequisitionsByDateRange(DateTime startDate, DateTime endDate)
        {
            return itemRequisitionContext.groupItemRequisitionsByDateRange(startDate, endDate);
        }

        public void Save(ItemRequisition itemRequisition)
        {
            //Check if itemCategory is in DB
            var ir = itemRequisitionContext.Get(itemRequisition.Id);

            //If not in DB, create new
            if (ir == null)
                itemRequisitionContext.Add(itemRequisition);
            //If in DB, update
            else
            {
                ir.Quantity = itemRequisition.Quantity;
                ir.ItemId = itemRequisition.ItemId;
            }

            //Commit changes
            itemRequisitionContext.Commit();
        }

    }
}