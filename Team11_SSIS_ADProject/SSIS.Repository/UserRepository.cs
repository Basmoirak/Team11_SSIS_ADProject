using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.Models;
using Team11_SSIS_ADProject.SSIS.Contracts.Repositories;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.Models.Custom;

namespace Team11_SSIS_ADProject.SSIS.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ApplicationDbContext _context;

        public UserRepository()
        {
            this._context = new ApplicationDbContext();
        }

        public IEnumerable<ApplicationUser> FindAllDepartmentEmployeesByDepartment(string departmentId)
        {
            return _context.Users
                .Where(user => user.DepartmentId == departmentId)
                .Where(r => r.Roles.Any(x => x.RoleId == UserRoles.Employee));
        }

        public ApplicationUser FindUserByEmail(string email)
        {
            return _context.Users.Where(x => x.Email == email).FirstOrDefault();
        }

        public bool FindIfUserExist(string email)
        {
            return _context.Users.Where(x => x.Email == email).FirstOrDefault() != null;
        }
    }
}