using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.Models;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models.Custom;

namespace Team11_SSIS_ADProject.Controllers
{
    public class HomeController : Controller
    {
        IUserService userService;
        IDepartmentDelegationService departmentDelegationService;
        public HomeController(IUserService userService, IDepartmentDelegationService departmentDelegationService)
        {
            this.userService = userService;
            this.departmentDelegationService = departmentDelegationService;
        }

        public ActionResult Index()
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var delegations = departmentDelegationService.GetAll().Where(x => x.UserId == userId).ToList();
                if (delegations != null)
                {
                    foreach (var item in delegations)
                    {
                        DateRangeHelper dateRangeHelper = new DateRangeHelper(item.StartDate, item.EndDate);
                        ApplicationDbContext context = new ApplicationDbContext();
                        var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                        //Check if the delegation period is now
                        bool check = dateRangeHelper.Includes(DateTime.Now);
                        if (check)
                        {
                            userManager.AddToRole(userId, "DepartmentHead");
                        }
                        else
                        {
                            userManager.RemoveFromRole(userId, "DepartmentHead");
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}