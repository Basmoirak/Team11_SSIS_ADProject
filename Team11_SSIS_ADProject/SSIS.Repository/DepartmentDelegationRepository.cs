using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Repository
{
    public class DepartmentDelegationRepository : Repository<DepartmentDelegation>, IDepartmentDelegationRepository
    {
        public IEnumerable<DepartmentDelegation> GetAllByDepartmentId(string id)
        {
            IEnumerable<DepartmentDelegation> dd = _context.DepartmentDelegations
                                                .Where(x => x.DepartmentId == id)
                                                .ToList();
            return dd;
        }
    }
}