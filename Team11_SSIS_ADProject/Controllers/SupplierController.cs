using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.Helpers;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.Service;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.Controllers
{
    [CustomAuthorize(Roles = CustomRoles.CanManageSupplier)]
    public class SupplierController : Controller
    {
        ISupplierService supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            this.supplierService = supplierService;
        }
        public ActionResult Create()
        {
            var supplier = new SupplierViewModel();
            return View("SupplierForm", supplier);
        }
        // GET: Supplier
        public ActionResult Index()
        {
            var viewModel = new SupplierViewModel()
            {
                Suppliers = supplierService.GetAll()
            };

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Save(Supplier supplier)
        {
            supplierService.Save(supplier);

            return RedirectToAction("Index", "Supplier");         
        }
        public ActionResult Edit(string id)
        {
            var s = supplierService.Get(id);
            var supplier = new SupplierViewModel()
            {
                SupplierCode = s.SupplierCode,
                SupplierName = s.SupplierName,
                SupplierPhone = s.SupplierPhone,
                SupplierAddress = s.SupplierAddress,
                SupplierFax = s.SupplierFax,
                SupplierGSTNumber = s.SupplierGSTNumber,
                SupplierContactName = s.SupplierContactName
            };

            return View("SupplierForm", supplier);
        }

        public ActionResult Delete(string id)
        {

            supplierService.Delete(id);

            return RedirectToAction("Index", "Supplier");
        }
    }
}