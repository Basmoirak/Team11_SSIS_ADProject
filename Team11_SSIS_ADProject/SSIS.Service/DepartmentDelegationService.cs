using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Service
{
    public class DepartmentDelegationService : IDepartmentDelegationService
    {
        IDepartmentDelegationRepository departmentDelegationContext;
        public DepartmentDelegationService(IDepartmentDelegationRepository departmentDelegationContext)
        {
            this.departmentDelegationContext = departmentDelegationContext;
        }
        public void Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public DepartmentDelegation Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DepartmentDelegation> GetAll()
        {
            return departmentDelegationContext.GetAll();
        }

        public void Save(DepartmentDelegation departmentDelegation)
        {
            departmentDelegationContext.Add(departmentDelegation);
            departmentDelegationContext.Commit();
        }
    }
}