using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Repository
{
    public class RequisitionRepository : Repository<Requisition>, IRequisitionRepository
    {
        public IEnumerable<Requisition> getAllPendingRequisitionsByDepartment(string departmentId)
        {
            return _context.Requisitions
                .Include("ItemRequisitions")
                .Where(x => x.DepartmentId == departmentId)
                .Where(x => x.Status == CustomStatus.PendingApproval);
        }
    }
}