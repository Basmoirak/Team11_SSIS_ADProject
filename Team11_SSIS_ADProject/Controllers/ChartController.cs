using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.Controllers
{
    
    public class ChartController : Controller
    {
        IChartService chartService;
        IItemService itemService;

        public ChartController(IChartService chartService, IItemService itemService)
        {
            this.chartService = chartService;
            this.itemService = itemService;
        }
        // GET: Chart
        public ActionResult Index()
        {
            var viewModel = new ItemViewModel()
            {
                Items = itemService.GetAll()
            };
            return View(viewModel);
        }
        public ActionResult GetInventories()
        {
            var inventories = chartService.GetInventories();
            return Json(new { inventories = inventories }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ItemRequisitionsThisWeek()
        {
            var iRThisWeek = chartService.ItemRequisitionsThisWeek();
            return Json(new { iRThisWeek = iRThisWeek }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ItemPurchaseOrdersThisWeek()
        {
            var iPoThisWeek = chartService.ItemPurchaseOrdersThisWeek();
            return Json(new { iPoThisWeek = iPoThisWeek }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ItemRequisitionsTrend(string id)
        {
            var iRs = chartService.ItemRequisitionsTrend(id);
            return Json(new { iRs = iRs }, JsonRequestBehavior.AllowGet);
        }
    }
}