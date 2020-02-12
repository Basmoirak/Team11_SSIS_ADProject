using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Contracts
{
    public interface IDepartmentDelegationService 
    {
        void Save(DepartmentDelegation departmentDelegation);
        DepartmentDelegation Get(string id);
        IEnumerable<DepartmentDelegation> GetAll();
        void Delete(string Id);
        IEnumerable<DepartmentDelegation> GetAllByDepartmentId(string Id);
    }
}