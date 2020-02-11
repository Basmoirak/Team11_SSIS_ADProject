using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.Models.Extensions;
using Team11_SSIS_ADProject.SSIS.ViewModels;
using Team11_SSIS_ADProject.Models;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models.Custom;

namespace Team11_SSIS_ADProject.Controllers
{
    public class HomeController : Controller
    {
        IUserService userService;
        IDepartmentDelegationService departmentDelegationService;

        IPurchaseOrderService purchaseOrderService;
        IStockAdjustmentService stockAdjustmentService;
        IDisbursementService disbursementService;
        IItemService itemService;
        IInventoryService inventoryService;

        IRequisitionService requisitionService;
        IItemDisbursementService itemDisbursementService;
        public HomeController(IUserService userService, IDepartmentDelegationService departmentDelegationService, IPurchaseOrderService purchaseOrderService, IStockAdjustmentService stockAdjustmentService, IDisbursementService disbursementService, IItemService itemService, IInventoryService inventoryService, IRequisitionService requisitionService, IItemDisbursementService itemDisbursementService)
        {
            this.userService = userService;
            this.departmentDelegationService = departmentDelegationService;

            //dashboard for store
            this.purchaseOrderService = purchaseOrderService;
            this.stockAdjustmentService = stockAdjustmentService;
            this.disbursementService = disbursementService;
            this.itemService = itemService;
            this.inventoryService = inventoryService;

            //dashboard for department
            this.requisitionService = requisitionService;
            this.itemDisbursementService = itemDisbursementService;

        }
        public ActionResult Index()
        {
            try
            {
                var userId = User.Identity.GetUserId();

                // Retrieve delegations for user where the status is active
                var delegations = departmentDelegationService.GetAll()
                    .Where(x => x.UserId == userId)
                    .Where(x => x.Status == CustomStatus.isActive).ToList();

                if (delegations != null)
                {
                    foreach (var item in delegations)
                    {
                        DateRangeHelper dateRangeHelper = new DateRangeHelper(item.StartDate, item.EndDate);
                        ApplicationDbContext context = new ApplicationDbContext();
                        var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                        //Check if the delegation period is now
                        bool check = dateRangeHelper.Includes(DateTime.Now);
                        if (check)
                        {
                            userManager.AddToRole(userId, "DepartmentHead");
                        }
                        else
                        {
                            userManager.RemoveFromRole(userId, "DepartmentHead");
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            if (User.IsInRole("StoreClerk") || User.IsInRole("StoreManager") || User.IsInRole("StoreSupervisor"))
            {
                var purchaseOrderCount = purchaseOrderService.GetAll().Where(po => po.Status == CustomStatus.PendingApproval).Count();
                ViewBag.Poc = purchaseOrderCount;
                var stockAdjustmentCount = stockAdjustmentService.GetAll().Where(sa => sa.Status == CustomStatus.PendingApproval).Count();
                ViewBag.Sac = stockAdjustmentCount;
                var disbursementCount = disbursementService.GetAll().Where(x => x.Status == CustomStatus.ForRetrieval).Count();
                ViewBag.D = disbursementCount;
                var itemCount = itemService.GetItemsLowerThanReorderLevel().Count();
                ViewBag.I = itemCount;

                var viewModel = new ItemViewModel()
                {
                    Items = itemService.GetAll()
                };
                return View("Index", viewModel);
            }
            if (User.IsInRole("Employee") || User.IsInRole("DepartmentHead"))
            {
                var requisitionCount = requisitionService.GetAll()
                .Where(x => x.DepartmentId == User.Identity.GetDepartmentId())
                .Where(r => r.Status == CustomStatus.PendingApproval).Count();
                ViewBag.R = requisitionCount;

                var collectionCount = itemDisbursementService.GetDepartmentCollection(User.Identity.GetDepartmentId()).Count();
                ViewBag.C = collectionCount;

                return View();
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}