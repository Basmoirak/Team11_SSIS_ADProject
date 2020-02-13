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
    public class PredictionController : Controller
    {
        IChartService chartService;
        IMLService mLService;
        IItemService itemService;

        public PredictionController(IChartService chartService, IMLService mLService, IItemService itemService)
        {
            this.chartService = chartService;
            this.mLService = mLService;
            this.itemService = itemService;
        }

        public ActionResult Index()
        {

            return View();
        }
        public ActionResult PredictionofDailyUsage(String id)
        {
            if (id.Length>9)
            {
                id = id.Substring(8); ;
            }
            else
            {
                id = id.Substring(6);
            }
            var s = mLService.Predict(Convert.ToInt32(id));
            List<String> items = new List<string>();
            List<float> usage = new List<float>();
            foreach (var item in s)
            {

                items.Add(itemService.Get(item.Key).ItemDescription);
                usage.Add(item.Value);
            }
            var mlviewmodel = new MlViewModel_2
            {
                Items = items,
                usage = usage
            };
            return Json(new { mlviewmodel }, JsonRequestBehavior.AllowGet);
        }
    }
}