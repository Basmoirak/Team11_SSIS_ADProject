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
    public class DepartmentController : ApiController
    {

        IItemDisbursementService itemDisbursementService;
        IRequisitionService requisitionService;
        IItemRequisitionService itemRequisitionService;

        public DepartmentController(IItemDisbursementService itemDisbursementService, IRequisitionService requisitionService, IItemRequisitionService itemRequisitionService)
        {
            this.itemDisbursementService = itemDisbursementService;
            this.requisitionService = requisitionService;
            this.itemRequisitionService = itemRequisitionService;
        }

        //Get Collection details by the caller's department for mobile
        [HttpPost]
        [Route("api/department/collection")]
        public IHttpActionResult GetDisbursementCollectionByDepartment([FromBody] EmailViewModel viewModel)
        {
            List<GroupedDepartmentCollections> collection = itemDisbursementService.GetDepartmentCollection(viewModel.DepartmentId).ToList();

            return Ok(collection);
        }

        //Get Requisitions requested by the caller's department for mobile
        [HttpPost]
        [Route("api/department/requisition")]
        public IHttpActionResult GetRequsitionsForDepartment([FromBody] EmailViewModel viewModel)
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

    }
}
