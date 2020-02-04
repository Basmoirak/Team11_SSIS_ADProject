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
    [Authorize]
    public class StockAdjustmentController : ApiController
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

        [Route("api/stockadjustments/getitems")]
        public IHttpActionResult GetAllItems()
        {
            var viewModel = itemService.GetAll().ToList();

            return Ok(viewModel);
        }

    }
}
