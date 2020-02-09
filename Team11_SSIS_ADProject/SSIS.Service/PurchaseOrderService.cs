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
            var purchaseOrder = purchaseOrderContext.Get(Id);
            purchaseOrderContext.Remove(purchaseOrder);
            purchaseOrderContext.Commit();
        }

        public PurchaseOrder Get(string id)
        {
            return purchaseOrderContext.Get(id);
        }

        public IEnumerable<PurchaseOrder> GetAll()
        {
            return purchaseOrderContext.GetAll();
        }

        public int getPendingOrderCount()
        {
            return purchaseOrderContext.getPendingOrderCount();
        }

        public void Save(PurchaseOrder purchaseOrder)
        {
            PurchaseOrder po = purchaseOrderContext.Get(purchaseOrder.Id);
            if(po == null)
            {
                purchaseOrderContext.Add(purchaseOrder);
            }
            else
            {
                po.Remark = purchaseOrder.Remark;
                po.Status = purchaseOrder.Status;
                po.ExpectedDate = purchaseOrder.ExpectedDate;
            }
            purchaseOrderContext.Commit();
        }
    }
}