using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.Helpers;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.Controllers
{
    [CustomAuthorize(Roles = CustomRoles.CanManageDisbursements)]
    public class DisbursementController : Controller
    {
        IDisbursementService disbursementService;
        IItemDisbursementService itemDisbursementService;

        public DisbursementController(IDisbursementService disbursementService, IItemDisbursementService itemDisbursementService)
        {
            this.disbursementService = disbursementService;
            this.itemDisbursementService = itemDisbursementService;
        }

        // GET: Disbursement
        public ActionResult Index()
        {
            var viewModel = new DisbursementRetrievalViewModel()
            {
                Disbursements = disbursementService.GetAll().Where(x => x.Status == CustomStatus.ForRetrieval).ToList(),
                GroupedItemDisbursements = itemDisbursementService.groupItemDisbursementByItemID()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Disburse()
        {
            var disbursements = disbursementService.GetAll()
                .Where(x => x.Status == CustomStatus.ForRetrieval)
                .ToList();

            foreach (var d in disbursements)
            {
                var disbursement = disbursementService.Get(d.Id);
                disbursement.Status = CustomStatus.ReadyForCollection;
                disbursementService.Save(disbursement);
            }

            return RedirectToAction("Index","Disbursement");
        }

        public ActionResult Collection()
        {
            var viewModel = new CollectionsViewModel()
            {
                groupedDepartmentCollections = itemDisbursementService.groupItemDisbursementByDepartment()
            };

            return View(viewModel);
        }
    }
}