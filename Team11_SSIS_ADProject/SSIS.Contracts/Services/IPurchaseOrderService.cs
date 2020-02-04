using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Contracts.Services
{
    public interface IPurchaseOrderService
    {
        void Save(PurchaseOrder purchaseOrder);
        PurchaseOrder Get(string id);
        IEnumerable<PurchaseOrder> GetAll();
        void Delete(string Id);
    }
}
