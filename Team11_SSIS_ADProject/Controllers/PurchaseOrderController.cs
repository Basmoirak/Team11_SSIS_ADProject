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
    public class PurchaseOrderController : Controller
    {
        IPurchaseOrderService purchaseOrderService;
        IItemPurchaseOrderService itemPurchaseOrderService;
        ISupplierService supplierService;
        IItemSupplierService itemSupplierService;
        public PurchaseOrderController(IPurchaseOrderService purchaseOrderService, IItemPurchaseOrderService itemPurchaseOrderService, ISupplierService supplierService)
        {
            this.purchaseOrderService = purchaseOrderService;
            this.itemPurchaseOrderService = itemPurchaseOrderService;
            this.supplierService = supplierService;
        }
        // GET: PurchaseOrder
        public ActionResult Index()
        {
            var purchaseOrderViewModel = new PurchaseOrderViewModel()
            {              
                PurchaseOrders = purchaseOrderService.GetAll().OrderByDescending(r => r.createdDateTime)
            };
            return View("Index", purchaseOrderViewModel);
        }
        public ActionResult SupplierList()
        {
            var supplierViewModel = new SupplierViewModel()
            {
                Suppliers = supplierService.GetAll()
            };
            return View("SupplierList", supplierViewModel);
        }
        public ActionResult PurchaseOrderForm(string id)
        {
            var purchaseOrderViewModel = new PurchaseOrderViewModel()
            {
                ItemSuppliers = itemSupplierService.GetItemsLowerThanReorderLevelBySupplier(id)
            };
            return View("PurchaseOrderForm", purchaseOrderViewModel);
        }
    }
}