﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.Helpers;
using Team11_SSIS_ADProject.Models;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.Models.Custom;
using Team11_SSIS_ADProject.SSIS.Models.Extensions;
using Team11_SSIS_ADProject.SSIS.Service;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.Controllers
{
    
    public class DepartmentController : Controller
    {
        public IDepartmentService departmentService;
        IDepartmentDelegationService departmentDelegationService;
        IUserService userService;
        public DepartmentController(IDepartmentService departmentService, IDepartmentDelegationService departmentDelegationService, IUserService userService)
        {
            this.departmentService = departmentService;
            this.departmentDelegationService = departmentDelegationService;
            this.userService = userService;
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

        [CustomAuthorize(Roles = CustomRoles.CanManageDepartmentAdmin)]
        public ActionResult Admin()
        {
            var departmentId = User.Identity.GetDepartmentId();
            var UsersContext = new ApplicationDbContext();
            var collectionPointList = new List<string>()
            {
                "Administration Building",
                "Science School",
                "Management School",
                "Medical School",
                "Engineering School",
                "University Hospital"
            };

            var adminViewModel = new DepartmentAdminViewModel()
            {
                User = UsersContext.Users
                    .Where(x => x.DepartmentId == departmentId)
                    .Where(x => x.Roles.Any(r => r.RoleId == UserRoles.Representative))
                    .FirstOrDefault(),
                CollectionPoint = departmentService.Get(departmentId).DepartmentCollectionPoint,
                CollectionPoints = collectionPointList,
                Users = UsersContext.Users
                    .Where(x => x.DepartmentId == departmentId)
                    .Where(x => x.Roles.Any(r => r.RoleId == UserRoles.Representative || r.RoleId == UserRoles.Employee)).ToList()
            };

            return View(adminViewModel);
        }

        [CustomAuthorize(Roles = CustomRoles.CanManageDepartmentAdmin)]
        public ActionResult SaveAdmin(DepartmentAdminViewModel viewModel)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var departmentId = User.Identity.GetDepartmentId();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //Update department collection point
            var department = departmentService.Get(departmentId);
            department.DepartmentCollectionPoint = viewModel.CollectionPoint;
            departmentService.Save(department);

            //Change current representative to employee role
            var user = context.Users
                                    .Where(x => x.DepartmentId == departmentId)
                                    .Where(x => x.Roles.Any(r => r.RoleId == UserRoles.Representative))
                                    .FirstOrDefault();
            userManager.RemoveFromRole(user.Id, "Representative");
            userManager.AddToRole(user.Id, "Employee");

            //Change new representative
            var newRepresentative = userService.FindUserByEmail(viewModel.User.Email);
            userManager.RemoveFromRole(newRepresentative.Id, "Employee");
            userManager.AddToRole(newRepresentative.Id, "Representative");

            context.SaveChanges();

            return RedirectToAction("Index", "Home");
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
                Users = UsersContext.Users
                        .Where(x => x.DepartmentId == departmentId)
                        .Where(x => x.Roles.Any(r => r.RoleId == UserRoles.Employee || r.RoleId == UserRoles.Representative))
                        .ToList(),
                DepartmentDelegations = departmentDelegationService.GetAllByDepartmentId(departmentId),
                DepartmentId = departmentId,
                Department = departmentService.Get(departmentId)
            };      
            return View("ManageDelegation", departmentDelegation);
        }

        [CustomAuthorize(Roles = CustomRoles.CanManageDepartmentDelegation)]
        public ActionResult SaveDelegation(DepartmentDelegation delegation)
        {
            // Get username
            var usersContext = new ApplicationDbContext();
            var username = usersContext.Users.Where(x => x.Id == delegation.UserId).FirstOrDefault().Email;

            // Save department delegation
            delegation.UserName = username;
            delegation.Status = CustomStatus.isActive;
            departmentDelegationService.Save(delegation);

            // Check if date is between date range
            var deptDelegation = departmentDelegationService.Get(delegation.Id);

            return RedirectToAction("Delegation");
        }

        public ActionResult EditDelegationStatus(string Id)
        {
            var delegation = departmentDelegationService.Get(Id);

            if(delegation.Status == CustomStatus.isActive)
            {
                delegation.Status = CustomStatus.isNotActive;
            }
            else if(delegation.Status == CustomStatus.isNotActive)
            {
                delegation.Status = CustomStatus.isActive;
            }

            departmentDelegationService.Save(delegation);

            return RedirectToAction("Delegation");
        }
        public ActionResult DelegationPendingList()
        {
            var departmentId = User.Identity.GetDepartmentId();
            var UsersContext = new ApplicationDbContext();
            var departmentDelegation = new DepartmentDelegationViewModel()
            {
                Users = UsersContext.Users
                        .Where(x => x.DepartmentId == departmentId)
                        .Where(x => x.Roles.Any(r => r.RoleId == UserRoles.Employee || r.RoleId == UserRoles.Representative))
                        .ToList(),
                DepartmentDelegations = departmentDelegationService.GetAllByDepartmentId(departmentId).Where(x => x.Status == CustomStatus.isNotActive)
                .Where(x=>x.StartDate.Date >= DateTime.Now.Date),
                DepartmentId = departmentId,
                Department = departmentService.Get(departmentId)
            };
            ViewBag.dD = "Showing Results for Pending List";
            return View("ManageDelegation", departmentDelegation);
        }
        public ActionResult DelegationActiveList()
        {
            var departmentId = User.Identity.GetDepartmentId();
            var UsersContext = new ApplicationDbContext();
            var departmentDelegation = new DepartmentDelegationViewModel()
            {
                Users = UsersContext.Users
                        .Where(x => x.DepartmentId == departmentId)
                        .Where(x => x.Roles.Any(r => r.RoleId == UserRoles.Employee || r.RoleId == UserRoles.Representative))
                        .ToList(),
                DepartmentDelegations = departmentDelegationService.GetAllByDepartmentId(departmentId).Where(x => x.Status == CustomStatus.isActive),
                DepartmentId = departmentId,
                Department = departmentService.Get(departmentId)
            };
            ViewBag.dD = "Showing Results for Active List";
            return View("ManageDelegation", departmentDelegation);
        }
        public ActionResult DelegationInactiveList()
        {
            var departmentId = User.Identity.GetDepartmentId();
            var UsersContext = new ApplicationDbContext();
            var departmentDelegation = new DepartmentDelegationViewModel()
            {
                Users = UsersContext.Users
                        .Where(x => x.DepartmentId == departmentId)
                        .Where(x => x.Roles.Any(r => r.RoleId == UserRoles.Employee || r.RoleId == UserRoles.Representative))
                        .ToList(),
                DepartmentDelegations = departmentDelegationService.GetAllByDepartmentId(departmentId).Where(x => x.Status == CustomStatus.isNotActive)
                .Where(x => x.EndDate.Date <= DateTime.Now.Date),
                DepartmentId = departmentId,
                Department = departmentService.Get(departmentId)
            };
            ViewBag.dD = "Showing Results for Inactive List";
            return View("ManageDelegation", departmentDelegation);
        }
    }
}