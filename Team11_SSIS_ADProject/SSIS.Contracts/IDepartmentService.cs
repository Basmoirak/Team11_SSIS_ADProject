using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Contracts
{
    public interface IDepartmentService
    {
        void Save(Department department);
        Department Get(string id);
        IEnumerable<Department> GetAll();
        void Delete(string Id);
    }
}