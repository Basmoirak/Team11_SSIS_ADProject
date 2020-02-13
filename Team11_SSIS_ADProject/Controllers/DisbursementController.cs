    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.Helpers;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;
using Team11_SSIS_ADProject.SSIS.Models.Extensions;
using Team11_SSIS_ADProject.SSIS.Contracts;

namespace Team11_SSIS_ADProject.Controllers
{
    //[CustomAuthorize(Roles = CustomRoles.CanManageDisbursements)]
    public class DisbursementController : Controller
    {
        IDisbursementService disbursementService;
        IItemDisbursementService itemDisbursementService;
        IInventoryService inventoryService;

        public DisbursementController(IDisbursementService disbursementService, IItemDisbursementService itemDisbursementService, IInventoryService inventoryService)
        {
            this.disbursementService = disbursementService;
            this.itemDisbursementService = itemDisbursementService;
            this.inventoryService = inventoryService;
        }

        // GET: Disbursement
        [CustomAuthorize(Roles = CustomRoles.CanManageDisbursements)]
        public ActionResult Index()
        {
            var viewModel = new DisbursementRetrievalViewModel()
            {
                Disbursements = disbursementService.GetAll().Where(x => x.Status == CustomStatus.ForRetrieval).ToList(),
                GroupedItemDisbursements = itemDisbursementService.groupItemDisbursementByItemID()
            };

            return View(viewModel);
        }

        public ActionResult Details(string id)
        {
            var viewModel = itemDisbursementService.GetAllByDisbursementId(id);

            return View(viewModel);
        }

        [CustomAuthorize(Roles = CustomRoles.CanManageDisbursements)]
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

            return Json(new { message = "Successfully Disbursed." });
        }

        [CustomAuthorize(Roles = CustomRoles.CanManageDisbursements)]
        public ActionResult StoreCollection()
        {
            var viewModel = new CollectionsViewModel()
            {
                groupedDepartmentCollections = itemDisbursementService.groupItemDisbursementByDepartment()
            };

            return View(viewModel);
        }

        [CustomAuthorize(Roles = CustomRoles.CanManageDepartmentCollection)]
        public ActionResult DepartmentCollection()
        {
            var departmentId = User.Identity.GetDepartmentId();

            var viewModel = new CollectionsViewModel()
            {
                groupedDepartmentCollections = itemDisbursementService.GetDepartmentCollection(departmentId)
            };

            return View(viewModel);
        }

        public ActionResult ConfirmStoreCollection(string departmentId)
        {
            // If Department Employee already confirmed
            var disbursementsConfirmedByDepartment = disbursementService
                .getAllDisbursementsByStatusAndDepartmentId(CustomStatus.DepartmentConfirmedCollection, departmentId)
                .ToList();

            foreach (var d in disbursementsConfirmedByDepartment)
            {
                var disbursement = disbursementService.Get(d.Id);
                disbursement.Status = CustomStatus.CollectionComplete;
                disbursementService.Save(disbursement);

                foreach (var id in d.ItemDisbursements)
                {
                    var inventoryItem = inventoryService.Get(id.ItemId);
                    inventoryItem.Quantity = inventoryItem.Quantity - id.AvailableQuantity;
                    inventoryService.Update(inventoryItem);
                }
            }

            // If Store clerk has not confirmed
            var disbursementsNotConfirmedByDepartment = disbursementService
                .getAllDisbursementsByStatusAndDepartmentId(CustomStatus.ReadyForCollection, departmentId)
                .ToList();

            foreach (var d in disbursementsNotConfirmedByDepartment)
            {
                var disbursement = disbursementService.Get(d.Id);
                disbursement.Status = CustomStatus.StoreConfirmedCollection;
                disbursementService.Save(disbursement);
            }

            return Json(new { message = "Success! You'll soon be redirected." });
        }

        [HttpPost]
        public ActionResult ConfirmDepartmentCollection()
        {
            var departmentId = User.Identity.GetDepartmentId();

            // If Store clerk already confirmed
            var disbursementsConfirmedByStoreClerk = disbursementService
                .getAllDisbursementsByStatusAndDepartmentId(CustomStatus.StoreConfirmedCollection, departmentId)
                .ToList();

            foreach (var d in disbursementsConfirmedByStoreClerk)
            {
                var disbursement = disbursementService.Get(d.Id);
                disbursement.Status = CustomStatus.CollectionComplete;
                disbursementService.Save(disbursement);

                foreach (var id in d.ItemDisbursements)
                {
                    var inventoryItem = inventoryService.Get(id.ItemId);
                    inventoryItem.Quantity = inventoryItem.Quantity - id.AvailableQuantity;
                    inventoryService.Update(inventoryItem);
                }
            }

            // If Store clerk has not confirmed
            var disbursementsNotConfirmedByStoreClerk = disbursementService
                .getAllDisbursementsByStatusAndDepartmentId(CustomStatus.ReadyForCollection, departmentId)
                .ToList();

            foreach (var d in disbursementsNotConfirmedByStoreClerk)
            {
                var disbursement = disbursementService.Get(d.Id);
                disbursement.Status = CustomStatus.DepartmentConfirmedCollection;
                disbursementService.Save(disbursement);
            }

            return Json (new { message = "Success! You'll soon be redirected." });
        }
    }
}