using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.Tests.Mocks;

namespace Team11_SSIS_ADProject.Tests.Controllers
{
    class BasketControllerTests
    {
        [TestMethod]
        public void CanAddBasketItem()
        {
            //Setup
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Item> items = new MockContext<Item>();

            var httpContext = new MockHttpContext();

            IBasketService basketService = new BasketService(items, baskets);
            var controller = new BasketController(basketService);
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            //Act
            //basketService.AddToBasket(httpContext, "1");

            controller.AddToBasket( "1");




            basketService.AddToBasket(httpContext, "1");

            Basket basket = baskets.Collection().FirstOrDefault();

            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.BasketItems.Count);
            Assert.AreEqual("1", basket.BasketItems.ToList().FirstOrDefault().ItemId);
        }

        [TestMethod]
        public void CanGetSummaryViewModel()
        {


        }

       
    }
}
