using Microsoft.VisualStudio.TestTools.UnitTesting;
using Team11_SSIS_ADProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team11_SSIS_ADProject.SSIS.Contracts;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.Service;
using Team11_SSIS_ADProject.SSIS.Repository;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.Controllers.Tests
{
    [TestClass()]
    public class DepartmentControllerTests
    {
        DepartmentService departmentService;
        DepartmentDelegationService departmentDelegationService;
        UserService userService;

        DepartmentRepository departmentRepository = new DepartmentRepository();
        DepartmentDelegationRepository departmentDelegationRepository = new DepartmentDelegationRepository();
        UserRepository userRepository = new UserRepository();

        DepartmentController departmentController;

        public DepartmentControllerTests()
        {
            departmentService = new DepartmentService(departmentRepository);
            departmentDelegationService = new DepartmentDelegationService(departmentDelegationRepository);
            userService = new UserService(userRepository);

            departmentController = new DepartmentController(departmentService, departmentDelegationService, userService);
        }

        [TestMethod()]
        public void CreateTest()
        {
            ActionResult result = this.departmentController.Create();
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void IndexTest()
        {
            ActionResult result = this.departmentController.Index();
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void SaveTest()
        {
            Department department = new Department()
            {
                DepartmentCode = "test_dep_1",
                DepartmentName = "test_dep_1",
                DepartmentContactName = "haizhou",
                DepartmentPhone = "99998888",
                DepartmentFax = "99998888",
                DepartmentHeadName = "haizhou",
                DepartmentRepresentative = "haizhou",
                DepartmentCollectionPoint = "school gate"
            };

            ActionResult result = this.departmentController.Save(department);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void EditTest()
        {
            var id = departmentController.departmentService.GetAll().First().Id;
            ActionResult result = this.departmentController.Edit(id);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var id = departmentController.departmentService.GetAll().First().Id;
            ActionResult result = this.departmentController.Delete(id);
            Assert.IsTrue(departmentController.itemService.GetAll().Select(i => i.Id).All(iid => iid != id));
        }
    }
}