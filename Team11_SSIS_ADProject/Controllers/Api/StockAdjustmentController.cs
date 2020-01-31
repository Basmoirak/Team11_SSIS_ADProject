using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.Controllers.Api
{
    [Authorize]
    public class StockAdjustmentController : ApiController
    {
        public List<ItemStockAdjustment> Get()
        {
            List<ItemStockAdjustment> stockAdjustments = new List<ItemStockAdjustment>
            {
                new ItemStockAdjustment{Id = "1", ItemId = "1", StockAdjustmentId = "2", StockMovement = 5, NewQuantity = 10, OldQuantity = 15},
                new ItemStockAdjustment{Id = "2", ItemId = "2", StockAdjustmentId = "2", StockMovement = 5, NewQuantity = 10, OldQuantity = 15},
                new ItemStockAdjustment{Id = "3", ItemId = "3", StockAdjustmentId = "2", StockMovement = 5, NewQuantity = 10, OldQuantity = 15},
                new ItemStockAdjustment{Id = "4", ItemId = "4", StockAdjustmentId = "2", StockMovement = 5, NewQuantity = 10, OldQuantity = 15},
                new ItemStockAdjustment{Id = "5", ItemId = "5", StockAdjustmentId = "2", StockMovement = 5, NewQuantity = 10, OldQuantity = 15}
            };

            return stockAdjustments;
        }
    }
}
