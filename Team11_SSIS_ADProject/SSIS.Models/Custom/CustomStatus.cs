using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.Models
{
    public static class CustomStatus
    {
        public const int Cancelled = 0;
        public const int PendingApproval = 1;
        public const int Approved = 2;
        public const int Rejected = 3;
        public const int ForRetrieval = 4;
        //Item Disbursement Status
        public const int PartialDisbursement = 5;
        public const int FullDisbursement = 6;
    }
}