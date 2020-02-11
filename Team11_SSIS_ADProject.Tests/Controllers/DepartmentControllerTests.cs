using Microsoft.VisualStudio.TestTools.UnitTesting;
using Team11_SSIS_ADProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team11_SSIS_ADProject.SSIS.Contracts;
using System.Web.Mvc;

namespace Team11_SSIS_ADProject.Controllers.Tests
{
    [TestClass()]
    public class DepartmentControllerTests
    {
        IDepartmentService departmentService;

        [TestMethod()]
        public void CreateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IndexTest()
        {

            // Arrange
            DepartmentController controller = new DepartmentController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void SaveTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }
    }
}