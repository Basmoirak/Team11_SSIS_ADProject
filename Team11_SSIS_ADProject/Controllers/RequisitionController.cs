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

namespace Team11_SSIS_ADProject.Controllers
{
    public class RequisitionController : Controller
    {
        IRequisitionService requisitionService;
        IItemRequisitionService itemRequisitionService;
        IItemService itemService;
        IDepartmentService departmentService;

        public RequisitionController(IDepartmentService departmentService, IItemService itemService, IRequisitionService requisitionService, IItemRequisitionService itemRequisitionService)
        {
            this.requisitionService = requisitionService;
            this.itemRequisitionService = itemRequisitionService;
            this.itemService = itemService;
            this.departmentService = departmentService;
        }
        // GET: Requisition
        public ActionResult Index()
        {
            var requistionViewModel = new RequisitionViewModel()
            {
                Items = itemService.GetAll(),
                Requisitions = requisitionService.GetAll().OrderByDescending(r => r.createdDateTime)
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
    }
}