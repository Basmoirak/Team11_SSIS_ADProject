using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.Repository;

namespace Team11_SSIS_ADProject.SSIS.Service
{
    public class DepartmentService : IDepartmentService
    {
        DepartmentRepository departmentContext;
        public DepartmentService()
        {
            this.departmentContext = new DepartmentRepository();
        }
        public void Save(Department department)
        {
            //Check if itemCategory is in DB
            var departmentInDb = departmentContext.Get(department.Id);

            //If not in DB, create new
            if (departmentInDb == null)
                departmentContext.Add(department);
            //If in DB, update
            else
            {
                departmentInDb.DepartmentName = department.DepartmentName;
            }

            //Commit changes
            departmentContext.Commit();
        }

        public Department Get(string id)
        {
            return departmentContext.Get(id);
        }

        public IEnumerable<Department> GetAll()
        {
            return departmentContext.GetAll();
        }

        public void Delete(string Id)
        {
            var department = departmentContext.Get(Id);
            departmentContext.Remove(department);
            departmentContext.Commit();
        }
    }
}