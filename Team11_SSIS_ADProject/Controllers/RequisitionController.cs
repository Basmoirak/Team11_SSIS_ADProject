using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;
using Team11_SSIS_ADProject.SSIS.Models.Extensions;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.Helpers;

namespace Team11_SSIS_ADProject.Controllers
{
    [CustomAuthorize(Roles = CustomRoles.CanManageRequisitions)]
    public class RequisitionController : Controller
    {
        IRequisitionService requisitionService;
        IItemRequisitionService itemRequisitionService;
        IItemService itemService;
        IDepartmentService departmentService;
        IDisbursementService disbursementService;
        IItemDisbursementService itemDisbursementService;
        IInventoryService InventoryService; 

        public RequisitionController(IDepartmentService departmentService, IItemService itemService, 
            IRequisitionService requisitionService, IItemRequisitionService itemRequisitionService,
            IDisbursementService disbursementService, IItemDisbursementService itemDisbursementService,
            IInventoryService inventoryService)
        {
            this.requisitionService = requisitionService;
            this.itemRequisitionService = itemRequisitionService;
            this.itemService = itemService;
            this.departmentService = departmentService;
            this.disbursementService = disbursementService;
            this.itemDisbursementService = itemDisbursementService;
            this.InventoryService = inventoryService;
        }

        // GET: Requisition
        public ActionResult Index()
        {
            var requistionViewModel = new RequisitionViewModel()
            {
                Items = itemService.GetAll(),
                Requisitions = requisitionService.GetAll()
                .Where(x => x.DepartmentId == User.Identity.GetDepartmentId())
                .OrderByDescending(r => r.createdDateTime)
                .OrderBy(r => r.Status)
            };
            return View("Index", requistionViewModel);
        }

        public ActionResult Create()
        {
            var requisitionViewModel = new RequisitionViewModel()
            {
                Items = itemService.GetAll()
            };
            return View("RequisitionForm", requisitionViewModel);
        }

        public ActionResult Save(Requisition requisition)
        {
            Requisition req = new Requisition
            {
                DepartmentId = User.Identity.GetDepartmentId(),
                Remark = requisition.Remark,
                Status = CustomStatus.PendingApproval
            };
            
            requisitionService.Save(req);

            //get the department 
            var department = departmentService.Get(req.DepartmentId);

            foreach (ItemRequisition ir in requisition.ItemRequisitions)
            {
                ItemRequisition itemRequisition = new ItemRequisition
                {
                    RequisitionId = req.Id,
                    Quantity = ir.Quantity,
                    ItemId = ir.ItemId
                };
                itemRequisitionService.Save(itemRequisition);
            }
            return Json(new { req = req, dept = department });
        }

        public ActionResult Details(string id)
        {
            var r = requisitionService.Get(id);
            var irs = itemRequisitionService.GetAllByRequisitionId(id);
            var requisitionViewModel = new RequisitionViewModel
            {
                Id = r.Id,
                Department = r.Department,
                createdDateTime = r.createdDateTime,
                Status = r.Status,
                Remark = r.Remark,
                ItemRequisitions = irs
            };
            return View("Details", requisitionViewModel);
        }

        [HttpPost]
        public ActionResult Approve(string id)
        {
            var requisition = requisitionService.Get(id);
            requisition.Status = CustomStatus.Approved;
            requisitionService.Save(requisition);


            var inventoryList = (List<Inventory>)InventoryService.GetAll();
            var itemRequisitionList = itemRequisitionService.GetAllByRequisitionId(requisition.Id);
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
            foreach (var req in itemRequisitionList)
            {
                var itemDisbursement = new ItemDisbursement()
                {
                    DisbursementId = disbursement.Id,
                    ItemId = req.ItemId,
                    RequestedQuantity = req.Quantity,
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

            return Json(new { id = requisition.Id});
        }

        [HttpPost]
        public ActionResult Reject(string id)
        {
            var requisition = requisitionService.Get(id);
            requisition.Status = CustomStatus.Rejected;
            requisitionService.Save(requisition);
            return Json(new { id = requisition.Id });
        }

        private List<ItemDisbursement> AllocateAvailableQtyAndStatus(
            List<ItemDisbursement> itemDisbursementList, List<Inventory> inventoryList, 
            List<ItemDisbursement> allItemDisbursements)
        {
            var newItemDisbursementList = itemDisbursementList;
            var newInventoryList = inventoryList;
            var newAllItemDisbursements = (List<ItemDisbursement>) allItemDisbursements;

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
                if(ActualQty - totalAvailable <= 0)
                {
                    ib.AvailableQuantity = 0;
                    ib.Status = CustomStatus.PartialDisbursement;
                }
                //If we have sufficient inventory for this request
                else if(ActualQty - totalRequested >= requestedQty)
                {
                    //Assign available quantity as requested quantity
                    ib.AvailableQuantity = ib.RequestedQuantity;
                    ib.Status = CustomStatus.FullDisbursement;

                    //Reduce inventory amount by requested qty
                    newInventoryList.Where(i => i.Id == ib.ItemId).FirstOrDefault().Quantity -= ib.RequestedQuantity;
                }
                //If insufficient inventory but greater than zero
                else if(ActualQty - totalRequested < requestedQty && ActualQty > 0)
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
        public ActionResult RequisitionPendingList()
        {
            var requistionViewModel = new RequisitionViewModel()
            {
                Items = itemService.GetAll(),
                Requisitions = requisitionService.GetAll()
                .Where(x => x.DepartmentId == User.Identity.GetDepartmentId())
                .Where(r=>r.Status==CustomStatus.PendingApproval)
                .OrderByDescending(r => r.createdDateTime)
                .OrderByDescending(r => r.Status)
            };
            ViewBag.RView = "Showing Results for Pending List";
            return View("Index", requistionViewModel);
        }
        public ActionResult RequisitionRejectedList()
        {
            var requistionViewModel = new RequisitionViewModel()
            {
                Items = itemService.GetAll(),
                Requisitions = requisitionService.GetAll()
                .Where(x => x.DepartmentId == User.Identity.GetDepartmentId())
                .Where(r => r.Status == CustomStatus.Rejected)
                .OrderByDescending(r => r.createdDateTime)
                .OrderByDescending(r => r.Status)
            };
            ViewBag.RView = "Showing Results for Rejected List";
            return View("Index", requistionViewModel);
        }
        public ActionResult RequisitionApprovedList()
        {
            var requistionViewModel = new RequisitionViewModel()
            {
                Items = itemService.GetAll(),
                Requisitions = requisitionService.GetAll()
                .Where(x => x.DepartmentId == User.Identity.GetDepartmentId())
                .Where(r => r.Status == CustomStatus.Approved)
                .OrderByDescending(r => r.createdDateTime)
                .OrderByDescending(r => r.Status)
            };
            ViewBag.RView = "Showing Results for Approved List";
            return View("Index", requistionViewModel);
        }
    }
}