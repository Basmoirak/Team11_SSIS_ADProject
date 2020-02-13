using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Extensions.ML;
using System.Net;
using System.Net.Http;
using Team11_SSIS_ADProject.SSIS.Models.ML;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Service;
using System.Web.Mvc;
using Microsoft.ML;
using Team11_SSIS_ADProject.SSIS.Contracts;

namespace Team11_SSIS_ADProject.SSIS.Service
{
    public class MLService:IMLService
    {
        ItemRequisitionService itemRequisitionService;
        RequisitionService requisitionService;
        IItemRepository itemService;
        MLContext mlContext;

        public MLService(ItemRequisitionService itemRequisitionService, RequisitionService requisitionService, IItemRepository itemService, MLContext mlContext)
        {
            this.itemRequisitionService = itemRequisitionService;
            this.requisitionService = requisitionService;
            this.itemService = itemService;
            this.mlContext = mlContext;
        }

        //single day usage data stores in one dictionary
        public IEnumerable<Dictionary<int, Dictionary<String, int>>> Dailyusage_history()
        {
            var reqs = requisitionService.GetAll().GroupBy(r => r.createdDateTime.Day).ToList();  //get requisitions grouped by day            
            Dictionary<int, Dictionary<String, int>> dic_of_day = new Dictionary<int, Dictionary<String, int>>();  //create a dic for saving daily usage (key is day,value is another dic<Item,Usage>)
            Dictionary<String, int> daily_usage_Items = new Dictionary<string, int>();   //create this dic for storing <Item,Usage>
            foreach (var rgrouped in reqs) //Traversing each group of grouped(by day) requsition list
            {
                foreach (var r in rgrouped)//Traversing each reuisition in grouped requsition list (by day)
                {
                    var items = itemRequisitionService.GetAllByRequisitionId(r.Id); // get all items in that requisition
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
                        list.Add(new DailyUsage_Train { ItemId = dicof_itemusage.Key, DailyUsage = dicof_itemusage.Value, OrderDay = e.Key, ItemDescription = itemService.Get(dicof_itemusage.Key).ItemDescription });
                        //yield return
                        //    new DailyUsage_Train { ItemId = dicof_itemusage.Key, DailyUsage = dicof_itemusage.Value, OrderDay = e.Key, ItemDescription = _itemService.Get(dicof_itemusage.Key).ItemDescription };

                    }
                }
            }
            list.AddRange(list);
            list.Add(new DailyUsage_Train { ItemId = "1", ItemDescription = "Clips Double 1", OrderDay = 8, DailyUsage = 65f });
            list.Add(new DailyUsage_Train { ItemId = "1", ItemDescription = "Clips Double 1", OrderDay = 7, DailyUsage = 45f });
            list.Add(new DailyUsage_Train { ItemId = "1", ItemDescription = "Clips Double 1", OrderDay = 6, DailyUsage = 35f });
            list.Add(new DailyUsage_Train { ItemId = "2", ItemDescription = "Clips Double 2", OrderDay = 8, DailyUsage = 73f });
            list.Add(new DailyUsage_Train { ItemId = "2", ItemDescription = "Clips Double 2", OrderDay = 7, DailyUsage = 63f });
            list.Add(new DailyUsage_Train { ItemId = "2", ItemDescription = "Clips Double 2", OrderDay = 2, DailyUsage = 45f });
            list.Add(new DailyUsage_Train { ItemId = "3", ItemDescription = "Clips Double 3/4", OrderDay = 2, DailyUsage = 35f });
            list.Add(new DailyUsage_Train { ItemId = "3", ItemDescription = "Clips Double 3/4", OrderDay = 3, DailyUsage = 35f });
            list.Add(new DailyUsage_Train { ItemId = "7", ItemDescription = "Envelope Brown (3x6)", OrderDay = 8, DailyUsage = 70f });
            list.Add(new DailyUsage_Train { ItemId = "7", ItemDescription = "Envelope Brown (3x6)", OrderDay = 7, DailyUsage = 66f });
            list.Add(new DailyUsage_Train { ItemId = "7", ItemDescription = "Envelope Brown (3x6)", OrderDay = 6, DailyUsage = 72f });
            list.Add(new DailyUsage_Train { ItemId = "7", ItemDescription = "Envelope Brown (3x6)", OrderDay = 14, DailyUsage = 56f });
            list.Add(new DailyUsage_Train { ItemId = "8", ItemDescription = "Envelope Brown (5x7)", OrderDay = 8, DailyUsage = 45f });
            list.Add(new DailyUsage_Train { ItemId = "9", ItemDescription = "Envelope White (3x6)", OrderDay = 8, DailyUsage = 55f });
            list.Add(new DailyUsage_Train { ItemId = "10", ItemDescription = "Envelope White (5x7)", OrderDay = 8, DailyUsage = 65f });
            list.AddRange(list);
            list.AddRange(list);
            list.AddRange(list);
            return list;
        }

        //Train data
        public ITransformer Train()
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
        public void Evaluate()
        {
            var model = Train();
            IDataView dataView = this.mlContext.Data.LoadFromEnumerable(Getdata().Skip((int)Getdata().Count() / 2)); //get test data
            var predictions = model.Transform(dataView);
            var metrics = mlContext.Regression.Evaluate(predictions, "Label", "Score");

            var R_Squared = metrics.RSquared;           //The closer its value is to 1, the better the model is. ∈(0,1)

        }

        //Predict daily usage of each item
        public Dictionary<String,float> Predict(int day)
        {
            var model = Train();
            var predictionFunction = this.mlContext.Model.CreatePredictionEngine<DailyUsage_Train, DailyUsage_Pred>(model);
            var ListofItems = itemService.GetAll();
            var DailyUsageOfEachItem = new Dictionary<String, float>();
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
                DailyUsageOfEachItem.Add(item.Id,pred);
            }
            return DailyUsageOfEachItem;

        }

        //calculate reorder-level
        public Dictionary<String, double> Pred_ROL(int day)
        {
            var listofprediction = Predict(day);
            var listofROL = new Dictionary<String, double>();
            foreach (var item in listofprediction)
            {
                listofROL.Add(item.Key,Math.Round(item.Value) * 3+ 50); //assuming lead time = 3 and saftey stock=50
            }
            return listofROL;
        }

        //calculate reorder-qty
        public Dictionary<String, double> Pred_RQty(int day)
        {
            var listofprediction = Predict(day);
            var listofRQty = new Dictionary<String, double>();
            foreach (var item in listofprediction)
            {
                listofRQty.Add(item.Key, Math.Round(item.Value) * 5); 
            }
            return listofRQty;
        }


    }
}