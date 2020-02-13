using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.ViewModels;
using Team11_SSIS_ADProject.SSIS.Models.Extensions;

namespace Team11_SSIS_ADProject.Controllers
{
    public class ReportController : Controller
    {

        IItemRequisitionService itemRequisitionService;
        IItemPurchaseOrderService itemPurchaseOrderService;
        IItemDisbursementService itemDisbursementService;
        public ReportController(IItemDisbursementService itemDisbursementService, IItemRequisitionService itemRequisitionService, IItemPurchaseOrderService itemPurchaseOrderService)
        {
            this.itemRequisitionService = itemRequisitionService;
            this.itemPurchaseOrderService = itemPurchaseOrderService;
            this.itemDisbursementService = itemDisbursementService;
        }
        // GET: Report

        public ActionResult Requisition()
        {
            return View(new RequisitionViewModel());
        }

        [HttpGet]
        public ActionResult SearchRequisition(DateTime startDate, DateTime endDate)
        {
            var viewModel = new RequisitionViewModel()
            {
                GroupedItemRequisitions = itemRequisitionService.groupItemRequisitionsByDateRange(startDate, endDate)
            };
            return View("Requisition", viewModel);
        }
        public ActionResult PurchaseOrder()
        {
            return View(new PurchaseOrderViewModel());
        }

        [HttpGet]
        public ActionResult SearchPurchaseOrder(DateTime startDate, DateTime endDate)
        {
            var viewModel = new PurchaseOrderViewModel()
            {
                GroupedItemPurchaseOrders = itemPurchaseOrderService.groupItemPurchaseOrdersByDateRange(startDate, endDate)
            };
            return View("PurchaseOrder", viewModel);
        }
        public ActionResult Collection()
        {
            return View(new DisbursementRetrievalViewModel());
        }
        [HttpGet]
        public ActionResult SearchCollection(DateTime startDate, DateTime endDate)
        {
            var viewModel = new DisbursementRetrievalViewModel()
            {
                GroupedItemDisbursements = itemDisbursementService.groupItemDisbursementsByDateRange(startDate, endDate)
            };
            return View("Collection", viewModel);
        }

        //For Individual Departments
        public ActionResult DepartmentRequisition()
        {
            return View(new RequisitionViewModel());
        }

        [HttpGet]
        public ActionResult DepartmentSearchRequisition(DateTime startDate, DateTime endDate)
        {
            var departmentId = User.Identity.GetDepartmentId();

            var viewModel = new RequisitionViewModel()
            {
                GroupedItemRequisitions = itemRequisitionService
                        .groupDepartmentItemRequisitionsByDateRange(startDate, endDate, departmentId)
            };
            return View("DepartmentRequisition", viewModel);
        }
    }
}