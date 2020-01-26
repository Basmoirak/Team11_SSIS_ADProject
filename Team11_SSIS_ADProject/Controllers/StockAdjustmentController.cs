using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;
using Microsoft.AspNet.Identity;

namespace Team11_SSIS_ADProject.Controllers
{
    public class StockAdjustmentController : Controller
    {
        IStockAdjustmentService stockAdjustmentService;
        IItemStockAdjustmentService itemStockAdjustmentService;
        IItemService itemService; 

        public StockAdjustmentController(IStockAdjustmentService stockAdjustmentService, 
            IItemStockAdjustmentService itemStockAdjustmentService, IItemService itemService)
        {
            this.stockAdjustmentService = stockAdjustmentService;
            this.itemStockAdjustmentService = itemStockAdjustmentService;
            this.itemService = itemService;
        }
        
        // GET: StockAdjustment View
        public ActionResult Index()
        {
            var viewModel = new StockAdjustmentViewModel()
            {
                StockAdjustments = stockAdjustmentService.GetAll(),
                Items = itemService.GetAll()
            };
            return View(viewModel);
        }

        public ActionResult Save(StockAdjustment stockAdjustment)
        {
            StockAdjustment sa = new StockAdjustment
            {
                CreatedBy = User.Identity.GetUserId(),
                Remarks = stockAdjustment.Remarks,
                Status = CustomStatus.PendingApproval
            };

            stockAdjustmentService.Save(sa);

            foreach (ItemStockAdjustment item in stockAdjustment.ItemStockAdjustments)
            {
                ItemStockAdjustment itemStockAdjustment = new ItemStockAdjustment
                {
                    StockAdjustmentId = sa.Id,
                    ItemId = item.ItemId,
                    StockMovement = item.StockMovement,
                    OldQuantity = stockAdjustmentService.GetItemQuantity(item.ItemId),
                    NewQuantity = stockAdjustmentService.GetItemQuantity(item.ItemId) + item.StockMovement
                };
                itemStockAdjustmentService.Save(itemStockAdjustment);
            }
            return Json(new { res = sa });
        }

        public ActionResult Details(string id)
        {
            //Get stockAdjustment
            var stockAdjustment = stockAdjustmentService.Get(id);
            //Get list of itemStockAdjustments
            var itemStockAdjustments = itemStockAdjustmentService.FindByStockAdjustmentId(stockAdjustment.Id);

            var stockAdjustmentViewModel = new StockAdjustmentViewModel()
            {
                Id = stockAdjustment.Id,
                Remarks = stockAdjustment.Remarks,
                itemStockAdjustments = itemStockAdjustments,
                Status = stockAdjustment.Status
            };

            return View(stockAdjustmentViewModel);
        }

        [HttpPost]
        public ActionResult Approve(string id)
        {
            //Update Stock Adjustment Status
            var stockAdjustment = stockAdjustmentService.Get(id);
            stockAdjustment.Status = CustomStatus.Approved;
            stockAdjustment.ApprovedBy = User.Identity.GetUserId();
            stockAdjustmentService.Save(stockAdjustment);

            // Update Inventory
            var itemStockAdjustments = itemStockAdjustmentService.FindByStockAdjustmentId(stockAdjustment.Id);
            foreach (var item in itemStockAdjustments)
            {
                //Take in itemId and stock movement to update inventory in database
                stockAdjustmentService.UpdateInventory(item.ItemId, item.StockMovement);
            }

            return Json(new { id = stockAdjustment.Id });
        }

        [HttpPost]
        public ActionResult Reject(string id)
        {
            //Update Stock Adjustment Status
            var stockAdjustment = stockAdjustmentService.Get(id);
            stockAdjustment.Status = CustomStatus.Rejected;
            stockAdjustment.ApprovedBy = User.Identity.GetUserId();
            stockAdjustmentService.Save(stockAdjustment);

            return Json(new { id = stockAdjustment.Id });
        }
    }
}