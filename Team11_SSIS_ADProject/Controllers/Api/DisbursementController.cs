using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.Controllers.Api
{
    public class DisbursementController : ApiController
    {
        IDisbursementService disbursementService;
        IItemDisbursementService itemDisbursementService;
        IInventoryService inventoryService;
        IDepartmentService departmentService;

        public DisbursementController(IDisbursementService disbursementService, IItemDisbursementService itemDisbursementService,
            IInventoryService inventoryService, IDepartmentService departmentService)
        {
            this.disbursementService = disbursementService;
            this.itemDisbursementService = itemDisbursementService;
            this.inventoryService = inventoryService;
            this.departmentService = departmentService;
        }

        //Get All Disbursements that are pending for retrievals for mobile
        [HttpPost]
        [Route("api/disbursement/retrieval")]
        public IHttpActionResult GetAllPendingRetrievals()
        {
            return Ok(itemDisbursementService.groupItemDisbursementByItemID().ToList());
        }

        [HttpPost]
        [Route("api/disbursement/confirmretrieval")]
        public IHttpActionResult ConfirmPendingRetrievals()
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
            MobileCollectionsViewModel viewModel = new MobileCollectionsViewModel
            {
                groupedDepartmentCollections = itemDisbursementService.groupItemDisbursementByDepartmentMobile().ToList()
            };

            return Ok(viewModel);
        }

        [HttpPost]
        [Route("api/disbursement/storecollection")]
        public IHttpActionResult GetStoreCollection([FromBody] EmailViewModel viewModel)
        {
            GroupedDepartmentCollections collection = itemDisbursementService.GetDepartmentCollectionForStore(viewModel.DepartmentId).FirstOrDefault();

            if (collection == null)
            {
                var dept = departmentService.Get(viewModel.DepartmentId);
                return Ok(new GroupedDepartmentCollections()
                {
                    CollectionPoint = dept.DepartmentCollectionPoint,
                    DepartmentName = dept.DepartmentName,
                    ItemDisbursements = null
                });
            }

            return Ok(collection);
        }

        //Confirm Collection by the caller's department for mobile
        [HttpPost]
        [Route("api/disbursement/confirmcollection")]
        public IHttpActionResult ConfirmDepartmentCollection([FromBody] EmailViewModel viewModel)
        {
            var departmentId = viewModel.DepartmentId;

            // If Department Employee already confirmed
            var disbursementsConfirmedByDepartment = disbursementService
                .getAllDisbursementsByStatusAndDepartmentId(CustomStatus.DepartmentConfirmedCollection, departmentId)
                .ToList();

            foreach (var d in disbursementsConfirmedByDepartment)
            {
                var disbursement = disbursementService.Get(d.Id);
                disbursement.Status = CustomStatus.CollectionComplete;
                disbursementService.Save(disbursement);

                foreach (var id in d.ItemDisbursements)
                {
                    var inventoryItem = inventoryService.Get(id.ItemId);
                    inventoryItem.Quantity = inventoryItem.Quantity - id.AvailableQuantity;
                    inventoryService.Update(inventoryItem);
                }
            }

            // If Store clerk has not confirmed
            var disbursementsNotConfirmedByDepartment = disbursementService
                .getAllDisbursementsByStatusAndDepartmentId(CustomStatus.ReadyForCollection, departmentId)
                .ToList();

            foreach (var d in disbursementsNotConfirmedByDepartment)
            {
                var disbursement = disbursementService.Get(d.Id);
                disbursement.Status = CustomStatus.StoreConfirmedCollection;
                disbursementService.Save(disbursement);
            }

            return Ok();
        }
    }
}
