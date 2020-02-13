using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.Models.ML
{
    public class DailyUsage_Pred
    {
        [ColumnName("Score")]
        public float DailyUsage;
    }
}