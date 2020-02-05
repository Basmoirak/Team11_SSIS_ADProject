using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.Controllers.Api
{
    public class DisbursementController : ApiController
    {
        IDisbursementService disbursementService;
        IItemDisbursementService itemDisbursementService; 

        public DisbursementController(IDisbursementService disbursementService, IItemDisbursementService itemDisbursementService)
        {
            this.disbursementService = disbursementService;
            this.itemDisbursementService = itemDisbursementService;
        }

        //Get All Disbursements that are pending for retrievals for mobile
        [HttpPost]
        [Route("api/disbursement/retrieval")]
        public IHttpActionResult GetAllPendingRetrievals()
        {
            DisbursementRetrievalMobileViewModel viewModel = new DisbursementRetrievalMobileViewModel
            {
                GroupedItemDisbursements = itemDisbursementService.groupItemDisbursementByItemID().ToList()
            };

            return Ok(viewModel);
        }

        [HttpPost]
        [Route("api/disbursement/approveretrieval")]
        public IHttpActionResult ApprovePendingRetrievals()
        {
            var disbursements = disbursementService.GetAll()
                .Where(x => x.Status == CustomStatus.ForRetrieval)
                .ToList();

            foreach (var d in disbursements)
            {
                var disbursement = disbursementService.Get(d.Id);
                disbursement.Status = CustomStatus.ReadyForCollection;
                disbursementService.Save(disbursement);
            }

            return Ok();
        }


        [HttpPost]
        [Route("api/disbursement/collection")]
        public IHttpActionResult GetAllCollection()
        {
            CollectionsViewModel viewModel = new CollectionsViewModel
            {
                groupedDepartmentCollections = itemDisbursementService.groupItemDisbursementByDepartment().ToList()
            };

            return Ok(viewModel);
        }
    }
}
