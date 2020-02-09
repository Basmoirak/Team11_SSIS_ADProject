using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.Controllers.Api
{
    //[Authorize]
    public class StockAdjustmentController : ApiController
    {
        IStockAdjustmentService stockAdjustmentService;
        IItemStockAdjustmentService itemStockAdjustmentService;

        public StockAdjustmentController(IStockAdjustmentService stockAdjustmentService,
            IItemStockAdjustmentService itemStockAdjustmentService)
        {
            this.stockAdjustmentService = stockAdjustmentService;
            this.itemStockAdjustmentService = itemStockAdjustmentService;
        }

        [Route("api/stockadjustments/pendingapproval")]
        public IHttpActionResult GetPendingApproval()
        {
            var viewModel = stockAdjustmentService.GetAll()
                                .Where(x => x.Status == CustomStatus.PendingApproval)
                                .ToList();

            return Ok(viewModel);
        }

        [HttpPost]
        [Route("api/stockadjustments/create")]
        public IHttpActionResult createStockAdjustment([FromBody] MobileStockAdjustment viewModel)
        {
            //Create new stock adjustment and update to pending approval
            StockAdjustment sa = new StockAdjustment
            {
                CreatedBy = viewModel.CreatedBy,
                Remarks = viewModel.Remarks,
                Status = CustomStatus.PendingApproval
            };

            stockAdjustmentService.Save(sa);

            //Create new itemStockAdjustment
            ItemStockAdjustment itemStockAdjustment = new ItemStockAdjustment
            {
                StockAdjustmentId = sa.Id,
                ItemId = viewModel.ItemId,
                StockMovement = viewModel.Movement,
                OldQuantity = stockAdjustmentService.GetItemQuantity(viewModel.ItemId),
                NewQuantity = stockAdjustmentService.GetItemQuantity(viewModel.ItemId)
            };

            itemStockAdjustmentService.Save(itemStockAdjustment);

            return Ok();
        }

        [HttpPost]
        [Route("api/stockadjustments/approve")]
        public IHttpActionResult Approve([FromBody] MobileStockAdjustment viewModel)
        {
            //Update Stock Adjustment Status
            var stockAdjustment = stockAdjustmentService.Get(viewModel.StockAdjustmentId);
            stockAdjustment.Status = CustomStatus.Approved;
            stockAdjustment.ApprovedBy = viewModel.CreatedBy;
            stockAdjustmentService.Save(stockAdjustment);

            // Update Inventory
            var itemStockAdjustments = itemStockAdjustmentService.FindByStockAdjustmentId(stockAdjustment.Id);
            foreach (var item in itemStockAdjustments)
            {
                //Take in itemId and stock movement to update inventory in database
                stockAdjustmentService.UpdateInventory(item.ItemId, item.StockMovement);
            }

            return Ok();
        }

        [HttpPost]
        [Route("api/stockadjustments/reject")]
        public IHttpActionResult Reject([FromBody] MobileStockAdjustment viewModel)
        {
            //Update Stock Adjustment Status
            var stockAdjustment = stockAdjustmentService.Get(viewModel.StockAdjustmentId);
            stockAdjustment.Status = CustomStatus.Rejected;
            stockAdjustment.ApprovedBy = viewModel.CreatedBy;
            stockAdjustmentService.Save(stockAdjustment);

            return Ok();
        }
    }
}
