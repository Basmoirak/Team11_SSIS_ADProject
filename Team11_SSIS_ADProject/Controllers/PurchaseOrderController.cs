using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;

namespace Team11_SSIS_ADProject.Controllers
{
    public class PurchaseOrderController : Controller
    {
        IPurchaseOrderService purchaseOrderService;
        IItemPurchaseOrderService itemPurchaseOrderService;
        public PurchaseOrderController(IPurchaseOrderService purchaseOrderService, IItemPurchaseOrderService itemPurchaseOrderService)
        {
            this.purchaseOrderService = purchaseOrderService;
            this.itemPurchaseOrderService = itemPurchaseOrderService;
        }
        // GET: PurchaseOrder
        public ActionResult Index()
        {
            return View();
        }
    }
}