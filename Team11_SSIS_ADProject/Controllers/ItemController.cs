using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.Helpers;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.Controllers
{
    [CustomAuthorize(Roles = CustomRoles.CanManageItems)]
    public class ItemController : Controller
    {
        IItemCategoryService itemCategoryService;
        IItemService itemService;
        ISupplierService supplierService;
        IItemSupplierService itemSupplierService;
        IInventoryService inventoryService;
        public ItemController(IInventoryService inventoryService, IItemSupplierService itemSupplierService, IItemCategoryService itemCategoryService, IItemService itemService, ISupplierService supplierService)
        {
            this.itemSupplierService = itemSupplierService;
            this.itemCategoryService = itemCategoryService;
            this.itemService = itemService;
            this.supplierService = supplierService;
            this.inventoryService = inventoryService;
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

            //creating new inventory row upon creating new item
            var inventory = new Inventory()
            {
                Id = item.Id,
                Quantity = 0
            };
            inventoryService.Save(inventory);

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

        public ActionResult ManageSuppliers(string id)
        {
            var item = itemService.Get(id);
            var suppliers = supplierService.GetAll();
            var suppliersByItem = itemSupplierService.GetSuppliersByItem(id);
            
            var itemViewModel = new ItemViewModel()
            {
                Id = item.Id,
                ItemDescription = item.ItemDescription,
                Suppliers = suppliers,
                SuppliersByItem = suppliersByItem
            };
            return View("ManageSuppliers", itemViewModel);
        }

        public ActionResult AssignSupplier(ItemSupplier itemSupplier)
        {
            itemSupplierService.Save(itemSupplier);
            
            return Json(new { message = "Added Successfully."});
        }

        public ActionResult FetchSuppliers(string id)
        {
            var suppliers = itemSupplierService.GetSuppliersByItem(id);
            int count = itemSupplierService.GetSuppliersByItem(id).Count();

            return Json(new
            {
                suppliers = suppliers.Select(x => new
                    {
                        Id = x.Id,
                        ItemId = x.ItemId,
                        Priority = x.Priority,
                        SupplierName = x.Supplier.SupplierName,
                        Price = x.Price
                    }
                ),
                count = count
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteSupplier(string id)
        {
            string itemId = itemSupplierService.Get(id).ItemId;
            int priority = itemSupplierService.Get(id).Priority;
            itemSupplierService.Delete(id);
            //reset the priority after deleting one supplier
            ResetPriority(itemId, priority);
            return Json(new { message = "Removed successfully."});
        }

        //reset the priority
        public ActionResult ResetPriority(string itemId, int priority)
        {
            var suppliers = itemSupplierService.GetSuppliersByItem(itemId);
            
            suppliers.ToList().ForEach(x =>
            {
                if(x.Priority < priority)
                {
                    itemSupplierService.UpdatePriority(x.Id);
                }
            });
            return Json(new { suppliers = suppliers }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReorderPriority()
        {
            //string order = Request.Params["p[]"];
            string order = Request.Params["p[]"];
            //order of suppliers from frontend
            var orders = order.Split(',').ToList();

            
            //get suppliers by itemId
            var itemSuppliers = itemSupplierService.GetSuppliersByItem(orders[0]);

            orders.RemoveAt(0);
            //looping through itemSuppliers
            int i = 0;
            itemSuppliers.ToList().ForEach(x =>
            {
                //assigning updated order using service
                itemSupplierService.UpdatePriority(x.Id, Int32.Parse(orders[i]));
                i++;
            });

          
            return Json(new { message = "Priority Updated" }, JsonRequestBehavior.AllowGet);
        }
        
    }
}