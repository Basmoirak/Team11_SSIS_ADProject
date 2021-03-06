﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Contracts
{
    public interface IItemSupplierRepository : IRepository<ItemSupplier>
    {
        double GetItemPriceBySupplierIdAndItemId(string itemId, string supplierId);
    }
}
