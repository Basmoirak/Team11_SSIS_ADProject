using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.Helpers;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.Controllers
{
    [CustomAuthorize(Roles = CustomRoles.CanManageItems)]
    public class ItemController : Controller
    {
        IItemCategoryService itemCategoryService;
        IItemService itemService;
        public ItemController(IItemCategoryService itemCategoryService, IItemService itemService)
        {
            this.itemCategoryService = itemCategoryService;
            this.itemService = itemService;
        }

        public ActionResult Create()
        {
            var item = new ItemViewModel()
            {
                ItemCategories = itemCategoryService.GetCategoryList()
            };
            return View("ItemForm", item);
        }
        [HttpPost]
        public ActionResult Save(Item item)
        {
            if(Request.Files.Count > 0)
            {
                HttpPostedFileBase postedFile = Request.Files[0];

                if(postedFile.ContentLength > 0)
                {
                    //extract the file name
                    string fileName = System.IO.Path.GetFileName(postedFile.FileName);

                    //define the image file path
                    string filePath = "~/Uploads/" + fileName;

                    //save the image in folder
                    postedFile.SaveAs(Server.MapPath(filePath));

                    item.ImagePath = filePath;
                }
            }
            itemService.Save(item);

            return RedirectToAction("Index", "Item");
        }
        public ActionResult Index()
        {
            var item = new ItemViewModel()
            {
                Items = itemService.GetAll()
            };
            return View(item);
        }
        public ActionResult Edit(string id)
        {
            var s = itemService.Get(id);
            var item = new ItemViewModel()
            {
                ItemNumber = s.ItemNumber,
                ItemDescription = s.ItemDescription,
                ItemCategoryId = s.ItemCategoryId,
                ItemReorderLevel = s.ItemReorderLevel,
                ItemReorderQty = s.ItemReorderQty,
                ItemUnit = s.ItemUnit,
                ImagePath = s.ImagePath,
                ItemCategories = itemCategoryService.GetCategoryList()
            };
            return View("ItemForm", item);
        }

        public ActionResult Delete(string id)
        {
            itemService.Delete(id);
            return RedirectToAction("Index", "Item");
        }
        
    }
}