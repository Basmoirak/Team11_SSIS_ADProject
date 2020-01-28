using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Contracts.Services
{
    public interface IDisbursementService
    {
        void Save(Disbursement disbursement);
        Disbursement Get(string id);
        IEnumerable<Disbursement> GetAll();
        void Delete(string Id);
        IEnumerable<ItemDisbursement> getAllItemDisbursementsByStatus(int status);
    }
}
