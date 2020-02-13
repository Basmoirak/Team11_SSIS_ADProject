using Microsoft.VisualStudio.TestTools.UnitTesting;

using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.Tests.Mocks;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.Controllers.Tests
{
    [TestClass()]
    public class RequisitionControllerTests
    {
        IRequisitionService requisitionService;
        IItemRequisitionService itemRequisitionService;
        IItemService itemService;
        IDepartmentService departmentService;
        IDisbursementService disbursementService;
        IItemDisbursementService itemDisbursementService;
        IInventoryService inventoryService;

        [TestMethod()]
        public void IndexTest()
        {
            IRepository<Requisition> requisitionContext = new MockContext<Requisition>();

            requisitionContext.Add(new Requisition());

            RequisitionController requisitionController = new RequisitionController(departmentService, itemService, requisitionService, itemRequisitionService, disbursementService, itemDisbursementService, inventoryService);

            var result = requisitionController.Index() as ViewResult;
            var viewModel = (RequisitionViewModel)result.ViewData.Model;
            // Assert
            Assert.IsNotNull(viewModel.Requisitions);
        }
  
        [TestMethod()]
        public void SaveTest()
        {
     
            Requisition requisition = new Requisition()
            {
                Id = "1",
                DepartmentId = "2"
            };
            RequisitionController requisitionController = new RequisitionController(departmentService, itemService, requisitionService, itemRequisitionService, disbursementService, itemDisbursementService, inventoryService);

            ActionResult result = requisitionController.Save(requisition);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            RequisitionController requisitionController = new RequisitionController(departmentService, itemService, requisitionService, itemRequisitionService, disbursementService, itemDisbursementService, inventoryService);
            ActionResult result = requisitionController.Details("r-1");

            //ASSERT
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
        }

        [TestMethod()]
        public void ApproveTest()
        {
            RequisitionController requisitionController = new RequisitionController(departmentService, itemService, requisitionService, itemRequisitionService, disbursementService, itemDisbursementService, inventoryService);
            Requisition requisition = new Requisition()
            {
                Id = "1",
                Status = 0
            };
            requisitionController.Approve(requisition.Id);
            Assert.AreEqual(CustomStatus.Approved, requisition.Status);
        }

        [TestMethod()]
        public void RejectTest()
        {
            RequisitionController requisitionController = new RequisitionController(departmentService, itemService, requisitionService, itemRequisitionService, disbursementService, itemDisbursementService, inventoryService);
            Requisition requisition = new Requisition()
            {
                Id = "1",
                Status = 0
            };
            requisitionController.Approve(requisition.Id);
            Assert.AreEqual(CustomStatus.Rejected, requisition.Status);
        }

    }
}