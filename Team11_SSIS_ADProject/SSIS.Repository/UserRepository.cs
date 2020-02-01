using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.Models;
using Team11_SSIS_ADProject.SSIS.Contracts.Repositories;

namespace Team11_SSIS_ADProject.SSIS.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ApplicationDbContext _context;

        public UserRepository()
        {
            this._context = new ApplicationDbContext();
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