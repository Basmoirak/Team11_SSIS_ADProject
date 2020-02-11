using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.Models.ML
{
    public class DailyUsage_Train
    {
        [LoadColumn(0)]
        public String ItemId;

        [LoadColumn(1)]
        public String ItemDescription;
        [LoadColumn(2)]
        public float OrderDay;

        [LoadColumn(3)]
        public float DailyUsage;

    }
}