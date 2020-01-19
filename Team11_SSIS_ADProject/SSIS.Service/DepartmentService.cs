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
        IDepartmentRepository departmentContext;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            this.departmentContext = departmentRepository;
        }
        public void Save(Department department)
        {
            Department d = departmentContext.Get(department.Id);
            if (d == null)
            {
                departmentContext.Add(department);
            }
            else
            {
                d.DepartmentCode = department.DepartmentCode;
                d.DepartmentName = department.DepartmentName;
                d.DepartmentContactName = department.DepartmentContactName;
                d.DepartmentPhone = department.DepartmentPhone;
                d.DepartmentFax = department.DepartmentFax;
                d.DepartmentHeadName = department.DepartmentHeadName;
                d.DepartmentRepresentative = department.DepartmentRepresentative;           
            }
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