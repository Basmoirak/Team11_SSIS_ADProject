using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Models.ML;
using System.Threading.Tasks;

namespace Team11_SSIS_ADProject.SSIS.Contracts.Services
{
    public interface IMLService
    {
        Dictionary<String,double> Pred_ROL(int day);
        Dictionary<String, double> Pred_RQty();
        void Evaluate();
        Dictionary<String, float> Predict(int day);
    }
}