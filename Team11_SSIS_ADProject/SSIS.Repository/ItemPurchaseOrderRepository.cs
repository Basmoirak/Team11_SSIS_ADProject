using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts.Repositories;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Repository
{

    public class ItemPurchaseOrderRepository : Repository<ItemPurchaseOrder>, IItemPurchaseOrderRepository
    {
     
    }
}