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
        IDepartmentService departmentService;

        public DepartmentController(IDisbursementService disbursementService ,IItemDisbursementService itemDisbursementService, 
            IRequisitionService requisitionService, IItemRequisitionService itemRequisitionService, IInventoryService inventoryService,
            IItemService itemService, IDepartmentService departmentService)
        {
            this.disbursementService = disbursementService;
            this.itemDisbursementService = itemDisbursementService;
            this.requisitionService = requisitionService;
            this.itemRequisitionService = itemRequisitionService;
            this.inventoryService = inventoryService;
            this.itemService = itemService;
            this.departmentService = departmentService;
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
        public IHttpActionResult ApproveRequisition([FromBody] RequisitionMobileViewModel requisition)
        {
            try
            {
                // Update requisition status to Approved
                var req = requisitionService.Get(requisition.RequisitionId);
                req.Status = CustomStatus.Approved;
                requisitionService.Save(req);

                var inventoryList = (List<Inventory>)inventoryService.GetAll();
                var itemRequisitionList = itemRequisitionService.GetAllByRequisitionId(requisition.RequisitionId);
                var itemDisbursementList = new List<ItemDisbursement>();

                // Add Disbursement
                var disbursement = new Disbursement()
                {
                    DepartmentId = requisition.DepartmentId,
                    Status = CustomStatus.ForRetrieval
                };

                //Retrieve all itemDisbursements meant for retrieval
                var allItemDisbursements = (List<ItemDisbursement>)disbursementService.getAllItemDisbursementsByStatus(CustomStatus.ForRetrieval);

                //Allocate itemrequisition details to itemdisbursement
                foreach (var itemReq in itemRequisitionList)
                {
                    var itemDisbursement = new ItemDisbursement()
                    {
                        DisbursementId = disbursement.Id,
                        ItemId = itemReq.ItemId,
                        RequestedQuantity = itemReq.Quantity,
                        AvailableQuantity = 0
                    };
                    itemDisbursementList.Add(itemDisbursement);
                }

                disbursementService.Save(disbursement);

                //Allocate available quantity by inventory availability
                List<ItemDisbursement> finalItemDisbursementList = AllocateAvailableQtyAndStatus(itemDisbursementList, inventoryList, allItemDisbursements);
                foreach (var ib in finalItemDisbursementList)
                {
                    itemDisbursementService.Save(ib);
                }

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
        public IHttpActionResult RejectRequisition([FromBody] RequisitionMobileViewModel requisition)
        {
            try
            {
                var req = requisitionService.Get(requisition.RequisitionId);
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

            if(collection == null)
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

        private List<ItemDisbursement> AllocateAvailableQtyAndStatus(
            List<ItemDisbursement> itemDisbursementList, List<Inventory> inventoryList,
            List<ItemDisbursement> allItemDisbursements)
        {
            var newItemDisbursementList = itemDisbursementList;
            var newInventoryList = inventoryList;
            var newAllItemDisbursements = (List<ItemDisbursement>)allItemDisbursements;

            foreach (var ib in newItemDisbursementList)
            {
                //Find Actual Quantity from inventory list
                int ActualQty = newInventoryList.Where(i => i.Id == ib.ItemId).FirstOrDefault().Quantity;

                //Find Total requested quantity
                int totalRequested = newAllItemDisbursements
                    .Where(x => x.ItemId == ib.ItemId)
                    .Sum(x => x.RequestedQuantity);

                int totalAvailable = newAllItemDisbursements
                    .Where(x => x.ItemId == ib.ItemId)
                    .Sum(x => x.AvailableQuantity);

                int requestedQty = ib.RequestedQuantity;

                //If inventory is empty
                if (ActualQty - totalAvailable <= 0)
                {
                    ib.AvailableQuantity = 0;
                    ib.Status = CustomStatus.PartialDisbursement;
                }
                //If we have sufficient inventory for this request
                else if (ActualQty - totalRequested >= requestedQty)
                {
                    //Assign available quantity as requested quantity
                    ib.AvailableQuantity = ib.RequestedQuantity;
                    ib.Status = CustomStatus.FullDisbursement;

                    //Reduce inventory amount by requested qty
                    newInventoryList.Where(i => i.Id == ib.ItemId).FirstOrDefault().Quantity -= ib.RequestedQuantity;
                }
                //If insufficient inventory but greater than zero
                else if (ActualQty - totalRequested < requestedQty && ActualQty > 0)
                {
                    //Assign available quantity as the remaining inventory qty
                    ib.AvailableQuantity = ActualQty - totalRequested;
                    ib.Status = CustomStatus.PartialDisbursement;

                    //Reduce to inventory amount to zero
                    newInventoryList.Where(i => i.Id == ib.ItemId).FirstOrDefault().Quantity = 0;
                }

            }
            return newItemDisbursementList;
        }
    }
}
