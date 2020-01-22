using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.Controllers
{
    public class DepartmentDelegationController : Controller
    {
        IDepartmentDelegationService departmentDelegationService;
        public DepartmentDelegationController() { }
        public DepartmentDelegationController(IDepartmentDelegationService departmentDelegationService)
        {
            this.departmentDelegationService = departmentDelegationService;
        }

        // GET: DepartmentDelegation
        public ActionResult Index()
        {
            var viewModel = new DepartmentDelegationViewModel()
            {                    
                DepartmentDelegations = departmentDelegationService.GetAll()
            };

            return View(viewModel);
        }

        public ActionResult Create()
        {
            var departmentDelegation = new DepartmentDelegationViewModel();
            return View("DepartmentDelegationForm", departmentDelegation);
        }

        [HttpPost]
        public ActionResult Save(DepartmentDelegation departmentDelegation)
        {
            departmentDelegation.DepartmentId = User.Identity.GetDepartmentId();
            departmentDelegationService.Save(departmentDelegation);

            return RedirectToAction("Index", "DepartmentDelegation");
        }
        public ActionResult Edit(string id)
        {
            var dd = departmentDelegationService.Get(id);
            var departmentDelegation = new DepartmentDelegationViewModel()
            {
                StartDate = dd.StartDate,
                EndDate = dd.EndDate,
                Status = dd.Status,
                UserId = User.Identity.Name
            };

            return View("DepartmentDelegationForm", departmentDelegation);
        }

        public ActionResult Delete(string id)
        {

            departmentDelegationService.Delete(id);

            return RedirectToAction("Index", "DepartmentDelegation");
        }
    }
}