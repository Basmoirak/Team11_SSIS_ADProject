using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.Controllers
{
    public class ReportController : Controller
    {

        IItemRequisitionService itemRequisitionService;
        public ReportController(IItemRequisitionService itemRequisitionService)
        {
            this.itemRequisitionService = itemRequisitionService;
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
    }
}