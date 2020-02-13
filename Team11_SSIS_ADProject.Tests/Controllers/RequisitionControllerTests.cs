using Microsoft.VisualStudio.TestTools.UnitTesting;

using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models;
//using Team11_SSIS_ADProject.Tests.Mocks;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.ViewModels;
using Team11_SSIS_ADProject.SSIS.Service;
using Team11_SSIS_ADProject.SSIS.Repository;

namespace Team11_SSIS_ADProject.Controllers.Tests
{
    [TestClass()]
    public class RequisitionControllerTests
    {
        RequisitionService requisitionService;
        ItemRequisitionService itemRequisitionService;
        ItemService itemService;
        DepartmentService departmentService;
        DisbursementService disbursementService;
        ItemDisbursementService itemDisbursementService;
        InventoryService inventoryService;

        RequisitionRepository requisitionRepository = new RequisitionRepository();
        ItemRequisitionRepository itemRequisitionRepository = new ItemRequisitionRepository();
        ItemRepository itemRepository = new ItemRepository();
        DepartmentRepository departmentRepository = new DepartmentRepository();
        DisbursementRepository disbursementRepository = new DisbursementRepository();
        ItemDisbursementRepository itemDisbursementRepository = new ItemDisbursementRepository();
        InventoryRepository inventoryRepository = new InventoryRepository();

        public RequisitionControllerTests()
        {
            requisitionService = new RequisitionService(requisitionRepository);
            itemRequisitionService = new ItemRequisitionService(itemRequisitionRepository);
            itemService = new ItemService(itemRepository, new MLService(itemRequisitionService, requisitionService, itemRepository, new Microsoft.ML.MLContext()));
            departmentService = new DepartmentService(departmentRepository);
            disbursementService = new DisbursementService(disbursementRepository);
            itemDisbursementService = new ItemDisbursementService(itemDisbursementRepository);
            inventoryService = new InventoryService(inventoryRepository);
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