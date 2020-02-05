using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts.Repositories;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Service
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        IPurchaseOrderRepository purchaseOrderContext;
        public PurchaseOrderService(IPurchaseOrderRepository purchaseOrderContext)
        {
            this.purchaseOrderContext = purchaseOrderContext;
        }
        public void Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public PurchaseOrder Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PurchaseOrder> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save(PurchaseOrder purchaseOrder)
        {
            throw new NotImplementedException();
        }
    }
}