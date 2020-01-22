using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Repository
{
    public class ItemRequisitionRepository : Repository<ItemRequisition>, IItemRequisitionRepository
    {
        //custom query to retreive itemrequisition by requisitionid
        public IEnumerable<ItemRequisition> GetAllByRequisitionId(string id)
        {
            IEnumerable<ItemRequisition> irs = _context.ItemRequisitions
                                                .Where(ir => ir.RequisitionId == id)
                                                .ToList();
            return irs;                            
        }
    }
}