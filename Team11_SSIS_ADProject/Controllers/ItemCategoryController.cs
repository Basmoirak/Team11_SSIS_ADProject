using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.Service;

namespace Team11_SSIS_ADProject.Controllers
{
    public class ItemCategoryController : Controller
    {
        IItemCategoryService itemCategoryService;

        public ItemCategoryController(IItemCategoryService itemCategoryService)
        {
            this.itemCategoryService = itemCategoryService;
        }

        // GET: ItemCategory
        public ActionResult Index()
        {
            IEnumerable<ItemCategory> itemCategories = itemCategoryService.GetAll();
            return View("Index", itemCategories);
        }

        public ActionResult Create()
        {
            var itemCategory = new ItemCategory();

            return View("ItemCategoryForm", itemCategory);
        }

        public ActionResult Edit(string id)
        {
            var itemCategory = itemCategoryService.Get(id);

            return View("ItemCategoryForm", itemCategory);
        }

        [HttpPost]
        public ActionResult Save(ItemCategory itemCategory)
        {
            itemCategoryService.Save(itemCategory);

            return RedirectToAction("Index", "ItemCategory");
        }

        public ActionResult Delete(string id)
        {
            var itemCategory = itemCategoryService.Get(id);

            itemCategoryService.Delete(id);

            return RedirectToAction("Index", "ItemCategory");
        }
    }
}