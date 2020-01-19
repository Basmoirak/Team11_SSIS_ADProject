using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;

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

        public void Save(ItemRequisition itemRequisition)
        {
            throw new NotImplementedException();
        }
    }
}