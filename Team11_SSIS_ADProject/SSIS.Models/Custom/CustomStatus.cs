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
        public const int PartialDisbursement = 5;
        public const int FullDisbursement = 6;
        public const int ReadyForCollection = 7;
        public const int CollectionComplete = 8;
        public const int Completed = 9;
        public const int isActive = 10;
        public const int isNotActive = 11;
    }
}