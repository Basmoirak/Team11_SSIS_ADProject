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
using Team11_SSIS_ADProject.SSIS.Models.Extensions;

namespace Team11_SSIS_ADProject.Controllers.Api
{
    public class DepartmentController : ApiController
    {

        IDisbursementService disbursementService;
        IItemDisbursementService itemDisbursementService;
        IRequisitionService requisitionService;
        IItemRequisitionService itemRequisitionService;
        IInventoryService inventoryService;
        IItemService itemService;

        public DepartmentController(IDisbursementService disbursementService ,IItemDisbursementService itemDisbursementService, 
            IRequisitionService requisitionService, IItemRequisitionService itemRequisitionService, IInventoryService inventoryService,
            IItemService itemService)
        {
            this.disbursementService = disbursementService;
            this.itemDisbursementService = itemDisbursementService;
            this.requisitionService = requisitionService;
            this.itemRequisitionService = itemRequisitionService;
            this.inventoryService = inventoryService;
            this.itemService = itemService;
        }



        //Get Requisitions requested by the caller's department for mobile
        [HttpPost]
        [Route("api/department/requisition")]
        public IHttpActionResult GetRequisitionsForDepartment([FromBody] EmailViewModel viewModel)
        {
            List<Requisition> requisitions = requisitionService.getAllPendingRequisitionsByDepartment(viewModel.DepartmentId).ToList();

            var requisitionViewModel = requisitions.Select(requisition => new RequisitionMobileViewModel()
            {
                RequisitionId = requisition.Id,
                Remarks = requisition.Remark,
                Status = requisition.Status,
                ItemRequisitions = requisition.ItemRequisitions.Select(ir => new RequisitionDetailsMobileViewModel() {
                    ItemCode = ir.ItemId,
                    Description = ir.Item.ItemDescription,
                    Quantity = ir.Quantity
                }).ToList()
            }).ToList();

            return Ok(requisitionViewModel);
        }

        //Get itemrequisition details
        [HttpPost]
        [Route("api/department/requisitiondetails")]
        public IHttpActionResult GetRequisitionDetails([FromBody] RequisitionMobileViewModel viewModel)
        {
            List<ItemRequisition> itemRequisitions = itemRequisitionService.GetAllByRequisitionId(viewModel.RequisitionId).ToList();

            List<RequisitionDetailsMobileViewModel> itemRequsitionViewModel = itemRequisitions.Select(ir => new RequisitionDetailsMobileViewModel()
            {
                ItemCode = ir.ItemId,
                Description = itemService.Get(ir.ItemId).ItemDescription,
                Quantity = ir.Quantity
            }).ToList();

            return Ok(itemRequsitionViewModel);  
        }

        //Approve requisition
        [HttpPost]
        [Route("api/department/approverequisition")]
        public IHttpActionResult ApproveRequisition([FromBody] Requisition requisition)
        {
            try
            {
                var req = requisitionService.Get(requisition.Id);
                req.Status = CustomStatus.Approved;
                requisitionService.Save(req);
            }
            catch (Exception)
            {
                return BadRequest("Requisition already approved");
            }

            return Ok();
        }

        //Reject requisition
        [HttpPost]
        [Route("api/department/rejectrequisition")]
        public IHttpActionResult RejectRequisition([FromBody] Requisition requisition)
        {
            try
            {
                var req = requisitionService.Get(requisition.Id);
                req.Status = CustomStatus.Cancelled;
                requisitionService.Save(req);
            }
            catch (Exception)
            {
                return BadRequest("Requisition already approved");
            }

            return Ok();
        }

        //Get Collection details by the caller's department for mobile
        [HttpPost]
        [Route("api/department/collection")]
        public IHttpActionResult GetDisbursementCollectionByDepartment([FromBody] EmailViewModel viewModel)
        {
            GroupedDepartmentCollections collection = itemDisbursementService.GetDepartmentCollection(viewModel.DepartmentId).FirstOrDefault();

            return Ok(collection);
        }

        //Confirm Collection by the caller's department for mobile
        [HttpPost]
        [Route("api/department/confirmcollection")]
        public IHttpActionResult ConfirmDepartmentCollection([FromBody] EmailViewModel viewModel)
        {
            var departmentId = viewModel.DepartmentId;

            var disbursements = disbursementService
                .getAllDisbursementsByStatusAndDepartmentId(CustomStatus.ReadyForCollection, departmentId)
                .ToList();

            foreach (var d in disbursements)
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

            return Ok();
        }

    }
}
