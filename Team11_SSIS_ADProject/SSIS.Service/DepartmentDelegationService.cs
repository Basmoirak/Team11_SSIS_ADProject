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
            return departmentDelegationContext.Get(id);
        }

        public IEnumerable<DepartmentDelegation> GetAll()
        {
            return departmentDelegationContext.GetAll();
        }

        public void Save(DepartmentDelegation departmentDelegation)
        {
            DepartmentDelegation dd = departmentDelegationContext.Get(departmentDelegation.Id);

            if(dd == null)
            {
                departmentDelegationContext.Add(departmentDelegation);
            }
            else
            {
                dd.StartDate = departmentDelegation.StartDate;
                dd.EndDate = departmentDelegation.EndDate;
                dd.DepartmentId = departmentDelegation.DepartmentId;
                dd.UserId = departmentDelegation.UserId;
                dd.Status = departmentDelegation.Status;
            }

            departmentDelegationContext.Commit();
        }
    }
}