using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.SSIS.Contracts.Services
{
    public interface IItemDisbursementService
    {
        void Save(ItemDisbursement itemDisbursement);
        ItemDisbursement Get(string id);
        IEnumerable<ItemDisbursement> GetAll();
        void Delete(string Id);
        IQueryable<GroupedItemDisbursements> groupItemDisbursementByDepartment();
        IEnumerable<GroupedItemID> groupItemDisbursementByItemID();
    }
}
