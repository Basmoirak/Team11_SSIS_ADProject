using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Contracts.Repositories
{
    public interface IDisbursementRepository : IRepository<Disbursement>
    {
        IEnumerable<ItemDisbursement> getAllItemDisbursementsByStatus(int status);
        IEnumerable<Disbursement> getAllDisbursementsByStatusAndDepartmentId(int status, string departmentId);
    }
}
