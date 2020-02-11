using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team11_SSIS_ADProject.Models;

namespace Team11_SSIS_ADProject.SSIS.Contracts.Services
{
    public interface IUserService
    {
        ApplicationUser FindUserByEmail(string email);
        bool FindIfUserExist(string email);
        IEnumerable<ApplicationUser> FindAllDepartmentEmployeesByDepartment(string departmentId);
    }
}
