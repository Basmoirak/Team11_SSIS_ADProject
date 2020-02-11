using Microsoft.Extensions.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Team11_SSIS_ADProject.SSIS.Models.ML;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Service;
using System.Web.Mvc;
using Microsoft.ML;
using System.Threading;

namespace Team11_SSIS_ADProject.Controllers
{
    public class MLController : Controller
    {
        ItemRequisitionService _itemRequisitionService;
        RequisitionService _requisitionService;
        ItemService _itemService;
        MLService mLService;
        Dictionary<String, int> s;

        public MLController(ItemRequisitionService itemRequisitionService, RequisitionService requisitionService, ItemService itemservice,MLService mLService)
        //PredictionEnginePool<Reorder_level, Reorder_level_pred> predictionEnginePool)
        {
            _itemRequisitionService = itemRequisitionService;
            _requisitionService = requisitionService;
            _itemService = itemservice;
            this.mLService = mLService;
            //_predictionEnginePool = predictionEnginePool;
        }

        //single day usage data stores in one dictionary
        public IEnumerable<Dictionary<int, Dictionary<String, int>>> Dailyusage_history()
        {
            var reqs = _requisitionService.GetAll().GroupBy(r => r.createdDateTime.Day).ToList();  //get requisitions grouped by day            
            Dictionary<int, Dictionary<String, int>> dic_of_day = new Dictionary<int, Dictionary<String, int>>();  //create a dic for saving daily usage (key is day,value is another dic<Item,Usage>)
            Dictionary<String, int> daily_usage_Items = new Dictionary<string, int>();   //create this dic for storing <Item,Usage>
            foreach (var rgrouped in reqs) //Traversing each group of grouped(by day) requsition list
            {
                foreach (var r in rgrouped)//Traversing each reuisition in grouped requsition list (by day)
                {
                    var items = _itemRequisitionService.GetAllByRequisitionId(r.Id); // get all items in that requisition
                    foreach (var this_item in items)
                    {
                        if (!daily_usage_Items.ContainsKey(this_item.ItemId))
                        {
                            daily_usage_Items.Add(this_item.ItemId, this_item.Quantity);//saving daily usage of the item
                        }
                        else
                        {
                            daily_usage_Items[this_item.ItemId] += this_item.Quantity;//update each item usage from different requisition
                        }

                    }
                    if (!dic_of_day.ContainsKey(r.createdDateTime.Day))
                    {
                        dic_of_day.Add(r.createdDateTime.Day, daily_usage_Items); //save item daily usage based on day
                    }
                    else
                    {
                        dic_of_day[r.createdDateTime.Day] = daily_usage_Items;//update item daily usage based on day
                    }
                }
                yield return dic_of_day;
            }
            this.s = daily_usage_Items;
        }

        //save data in train model
        public List<DailyUsage_Train> Getdata()
        {
            List<DailyUsage_Train> list = new List<DailyUsage_Train>();
            var items_DailyUsage = Dailyusage_history();
            foreach (var dics in items_DailyUsage)
            {
                foreach (var e in dics)
                {

                    foreach (var dicof_itemusage in e.Value) 
                    {
                        list.Add(new DailyUsage_Train { ItemId = dicof_itemusage.Key, DailyUsage = dicof_itemusage.Value, OrderDay = e.Key, ItemDescription = _itemService.Get(dicof_itemusage.Key).ItemDescription });
                        //yield return
                        //    new DailyUsage_Train { ItemId = dicof_itemusage.Key, DailyUsage = dicof_itemusage.Value, OrderDay = e.Key, ItemDescription = _itemService.Get(dicof_itemusage.Key).ItemDescription };

                    }
                }
            }
            list.AddRange(list);
            list.Add(new DailyUsage_Train { ItemId = "1", ItemDescription = "Clips Double 1", OrderDay = 8, DailyUsage = 65f });
            list.Add(new DailyUsage_Train { ItemId = "1", ItemDescription = "Clips Double 1", OrderDay = 7, DailyUsage = 45f });
            list.Add(new DailyUsage_Train { ItemId = "1", ItemDescription = "Clips Double 1", OrderDay = 6, DailyUsage = 35f });
            list.Add(new DailyUsage_Train { ItemId = "1", ItemDescription = "Clips Double 1", OrderDay = 6, DailyUsage = 70f });
            list.Add(new DailyUsage_Train { ItemId = "2", ItemDescription = "Clips Double 2", OrderDay = 8, DailyUsage = 73f });
            list.Add(new DailyUsage_Train { ItemId = "2", ItemDescription = "Clips Double 2", OrderDay = 7, DailyUsage = 63f });
            list.Add(new DailyUsage_Train { ItemId = "2", ItemDescription = "Clips Double 2", OrderDay = 2, DailyUsage = 45f });
            list.Add(new DailyUsage_Train { ItemId = "7", ItemDescription = "Envelope Brown (3x6)", OrderDay = 8, DailyUsage = 70f });
            list.Add(new DailyUsage_Train { ItemId = "7", ItemDescription = "Envelope Brown (3x6)", OrderDay = 7, DailyUsage = 66f });
            list.Add(new DailyUsage_Train { ItemId = "7", ItemDescription = "Envelope Brown (3x6)", OrderDay = 6, DailyUsage = 72f });
            list.Add(new DailyUsage_Train { ItemId = "8", ItemDescription = "Envelope Brown (5x7)", OrderDay = 8, DailyUsage = 45f });
            list.Add(new DailyUsage_Train { ItemId = "9", ItemDescription = "Envelope White (3x6)", OrderDay = 8, DailyUsage = 55f });
            list.Add(new DailyUsage_Train { ItemId = "10", ItemDescription = "Envelope White (5x7)", OrderDay = 8, DailyUsage = 65f });
            list.AddRange(list);
            list.AddRange(list);
            list.AddRange(list);
            return list;
        }

        //Train data
        public ITransformer Train(MLContext mlContext)
        {

            var enumerableOfData = Getdata().Take((int)Getdata().Count() / 2);
            IDataView trainData = mlContext.Data.LoadFromEnumerable(enumerableOfData);
            var a = mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "ItemIdEncoded", inputColumnName: "ItemId");//new[] { new InputOutputColumnPair("ItemIdcoded", "ItemId") }
            var b = mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "ItemDescriptionEncoded", inputColumnName: "ItemDescription");
            var c = mlContext.Transforms.Concatenate("Features", "ItemIdEncoded", "ItemDescriptionEncoded", "OrderDay");
            var datapipe = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: "DailyUsage")
                .Append(a).Append(b).Append(c).Append(mlContext.Regression.Trainers.FastTree());
            ITransformer model = datapipe.Fit(trainData);
            return model;
        }

        //Evaluate data
        public void Evaluate(MLContext mlContext, ITransformer model)
        {

            IDataView dataView = mlContext.Data.LoadFromEnumerable(Getdata().Skip((int)Getdata().Count() / 2));
            var predictions = model.Transform(dataView);
            var metrics = mlContext.Regression.Evaluate(predictions, "Label", "Score");

            var R_Squared = metrics.RSquared;           //The closer its value is to 1, the better the model is. ∈(0,1)
            var RMS = metrics.RootMeanSquaredError;     //Lower RMS ,Better model

        }

        //Predict
        private List<float> Predict(MLContext mlContext, ITransformer model,int day)
        {
            var predictionFunction = mlContext.Model.CreatePredictionEngine<DailyUsage_Train, DailyUsage_Pred>(model);
            var ListofItems = _itemService.GetAll();
            var DailyUsageOfEachItem = new List<float>();
            foreach (var item in ListofItems)
            {
                var samplefortesting = new DailyUsage_Train()
                {
                    ItemId = item.Id,
                    ItemDescription = item.ItemDescription,
                    OrderDay = day,         //day of month
                    DailyUsage = 0        // To predict. 
                };
                var prediction = predictionFunction.Predict(samplefortesting);
                var pred = prediction.DailyUsage;
                DailyUsageOfEachItem.Add(pred);
            }
            return DailyUsageOfEachItem;
    
        }

        public ActionResult Index()
        {
            //var mlContext = new MLContext(seed: 1);
            //var model = Train(mlContext);
            //Evaluate(mlContext, model);           
            //var pred_list= Predict(mlContext, model, 5);
            var s=mLService.Predict(5);

            return Content("ss");
        }
        

    }
}