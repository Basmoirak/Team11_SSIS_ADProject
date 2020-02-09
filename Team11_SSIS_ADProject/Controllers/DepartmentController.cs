using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.Helpers;
using Team11_SSIS_ADProject.Models;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.Models.Extensions;
using Team11_SSIS_ADProject.SSIS.Service;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.Controllers
{
    
    public class DepartmentController : Controller
    {
        IDepartmentService departmentService;
        IDepartmentDelegationService departmentDelegationService;
        public DepartmentController(IDepartmentService departmentService, IDepartmentDelegationService departmentDelegationService)
        {
            this.departmentService = departmentService;
            this.departmentDelegationService = departmentDelegationService;
        }
        [CustomAuthorize(Roles = CustomRoles.CanManageDepartment)]
        public ActionResult Create()
        {
            var department = new DepartmentViewModel();
            return View("DepartmentForm", department);
        }
        // GET: Department
        [CustomAuthorize(Roles = CustomRoles.CanManageDepartment)]
        public ActionResult Index()
        {
            var viewModel = new DepartmentViewModel()
            {
                Departments = departmentService.GetAll()
            };

            return View(viewModel);
        }
        [HttpPost]
        [CustomAuthorize(Roles = CustomRoles.CanManageDepartment)]
        public ActionResult Save(Department department)
        {
            departmentService.Save(department);

            return RedirectToAction("Index", "Department");
        }
        [CustomAuthorize(Roles = CustomRoles.CanManageDepartment)]
        public ActionResult Edit(string id)
        {
            var d = departmentService.Get(id);
            var department = new DepartmentViewModel()
            {
                DepartmentCode = d.DepartmentCode,
                DepartmentName = d.DepartmentName,
                DepartmentContactName = d.DepartmentContactName,
                DepartmentPhone = d.DepartmentPhone,
                DepartmentFax = d.DepartmentFax,
                DepartmentHeadName = d.DepartmentHeadName,
                DepartmentRepresentative = d.DepartmentRepresentative
            };

            return View("DepartmentForm", department);
        }

        [CustomAuthorize(Roles = CustomRoles.CanManageDepartment)]
        public ActionResult Delete(string id)
        {

            departmentService.Delete(id);

            return RedirectToAction("Index", "Department");
        }
        [CustomAuthorize(Roles = CustomRoles.CanManageDepartmentDelegation)]
        public ActionResult Delegation()
        {
            var departmentId = User.Identity.GetDepartmentId();
            var UsersContext = new ApplicationDbContext();
            var departmentDelegation = new DepartmentDelegationViewModel()
            {
                //users = get the department id of currently logged in user
                //select all users with that id
                Users = UsersContext.Users.ToList().Where(x => x.DepartmentId == departmentId),
                DepartmentDelegations = departmentDelegationService.GetAll(),
                DepartmentId = departmentId,
                Department = departmentService.Get(departmentId)
                
            };      
            return View("ManageDelegation", departmentDelegation);
        }
        [CustomAuthorize(Roles = CustomRoles.CanManageDepartmentDelegation)]
        public ActionResult SaveDelegation(DepartmentDelegation delegation)
        {
   
            departmentDelegationService.Save(delegation);
            return RedirectToAction("Delegation");
        }

    }
}