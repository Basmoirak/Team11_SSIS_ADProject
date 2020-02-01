using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Team11_SSIS_ADProject.SSIS.Contracts.Repositories;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models.Custom;
using Team11_SSIS_ADProject.SSIS.Service;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.Controllers.Api
{

    [Authorize]
    public class UserController : ApiController
    {
        UserService userService;

        public UserController()
        {
            this.userService = new UserService();
        }

        [HttpPost]
        [Route("api/users/role")]
        public IHttpActionResult GetRole([FromBody] EmailViewModel viewModel)
        {
            //Check if User Exists, if not return not found
            if (!userService.FindIfUserExist(viewModel.Email))
                return NotFound();
            else
            {
                //Retrieve User
                string rolename = null;
                var user = userService.FindUserByEmail(viewModel.Email);

                    if (user.Roles.FirstOrDefault().RoleId == UserRoles.Employee) rolename = "Employee"; 
                    if (user.Roles.FirstOrDefault().RoleId == UserRoles.DepartmentHead) rolename = "DepartmentHead"; 
                    if (user.Roles.FirstOrDefault().RoleId == UserRoles.StoreClerk) rolename = "StoreClerk"; 
                    if (user.Roles.FirstOrDefault().RoleId == UserRoles.StoreSupervisor) rolename = "StoreSupervisor"; 
                    if (user.Roles.FirstOrDefault().RoleId == UserRoles.StoreManager) rolename = "StoreManager"; 
                    if (user.Roles.FirstOrDefault().RoleId == UserRoles.Admin) rolename = "Admin";

                //Add role to viewmodel
                viewModel.RoleName = rolename;
                return Ok(viewModel);
            }
        }
    }
}
