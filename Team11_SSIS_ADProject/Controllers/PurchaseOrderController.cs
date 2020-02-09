using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.Controllers
{
    public class PurchaseOrderController : Controller
    {
        IPurchaseOrderService purchaseOrderService;
        IItemPurchaseOrderService itemPurchaseOrderService;
        ISupplierService supplierService;
        IItemService itemService;
        IItemSupplierService itemSupplierService;
        IInventoryService inventoryService;
        public PurchaseOrderController(IInventoryService inventoryService, IItemSupplierService itemSupplierService, IItemService itemService, IPurchaseOrderService purchaseOrderService, IItemPurchaseOrderService itemPurchaseOrderService, ISupplierService supplierService)
        {
            this.purchaseOrderService = purchaseOrderService;
            this.itemPurchaseOrderService = itemPurchaseOrderService;
            this.supplierService = supplierService;
            this.itemService = itemService;
            this.itemSupplierService = itemSupplierService;
            this.inventoryService = inventoryService;
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
        public ActionResult PurchaseOrderForm()
        {
            var purchaseOrderViewModel = new PurchaseOrderViewModel()
            {
                ItemPurchaseOrderViewModels = itemService.GetItemsLowerThanReorderLevel(),
                PendingOrderCount = purchaseOrderService.getPendingOrderCount()
            };
            return View("PurchaseOrderForm", purchaseOrderViewModel);
        }
        public ActionResult GetItemPriceBySupplierIdAndItemId(string itemId, string supplierId)
        {
            double price = itemSupplierService.GetItemPriceBySupplierIdAndItemId(itemId, supplierId);
            return Json(new { price = price }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Save(IEnumerable<ItemPurchaseOrderViewModel> itemPurchaseOrders)
        {
            //empty dictionary list
            Dictionary<string, string> suppliers = new Dictionary<string, string>();

            itemPurchaseOrders.ToList().ForEach(x =>
            {
               if(!suppliers.ContainsValue(x.SupplierId) || suppliers.Count() == 0)
               {
                    PurchaseOrder po = new PurchaseOrder()
                    {
                        ExpectedDate = DateTime.Parse("2012/12/12"),
                        SupplierId = x.SupplierId,
                        Status = CustomStatus.PendingApproval
                    };
                    purchaseOrderService.Save(po);

                    suppliers.Add(po.Id, x.SupplierId);

                    ItemPurchaseOrder iPo = new ItemPurchaseOrder()
                    {
                        PurchaseOrderId = po.Id,
                        ItemId = x.ItemId,
                        Quantity = x.ItemReorderQty,
                        Price = x.Price
                    };
                    itemPurchaseOrderService.Save(iPo);
                }
                else if(suppliers.ContainsValue(x.SupplierId) || suppliers.Count() > 0)
                {
                    ItemPurchaseOrder iPo = new ItemPurchaseOrder()
                    {
                        PurchaseOrderId = suppliers.FirstOrDefault(z => z.Value == x.SupplierId).Key,
                        ItemId = x.ItemId,
                        Quantity = x.ItemReorderQty,
                        Price = x.Price
                    };
                    itemPurchaseOrderService.Save(iPo);
                }
            });   
            return Json(new { message = "Successfully created." });
        }
        public ActionResult Details(string id)
        {
            var purchaseOrder = purchaseOrderService.Get(id);
            var itemPurchaseOrders = itemPurchaseOrderService.GetAll().Where(x => x.PurchaseOrderId == purchaseOrder.Id).ToList();
            PurchaseOrderViewModel purchaseOrderViewModel = new PurchaseOrderViewModel()
            {
                Id = purchaseOrder.Id,
                Supplier = purchaseOrder.Supplier,
                ItemPurchaseOrders = itemPurchaseOrders,
                ExpectedDate = purchaseOrder.ExpectedDate,
                Status = purchaseOrder.Status,
                Remark = purchaseOrder.Remark
            };
            return View("Details", purchaseOrderViewModel);
        }
        [HttpPost]
        public ActionResult Confirm(string id)
        {
            var purchaseOrder = purchaseOrderService.Get(id);
            purchaseOrder.Status = CustomStatus.Completed;
            purchaseOrderService.Save(purchaseOrder);

            //Increse inventory value -- need to be implemented

            //loop through itempurchaseorders
            var iPs = itemPurchaseOrderService.GetAll().Where(x => x.PurchaseOrderId == id);
            foreach(var iP in iPs)
            {
                var inventoryItem = inventoryService.Get(iP.ItemId);
                inventoryItem.Quantity = inventoryItem.Quantity + iP.Quantity;
                inventoryService.Update(inventoryItem);
            }

            return Json(new { message = "Order has been confirmed." });
        }
        [HttpPost]
        public ActionResult Cancel(string id)
        {
            var purchaseOrder = purchaseOrderService.Get(id);
            purchaseOrder.Status = CustomStatus.Cancelled;
            purchaseOrderService.Save(purchaseOrder);

            return Json(new { message = "Order has been cancelled." });
        }
    }
}