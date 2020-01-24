using System;
using Team11_SSIS_ADProject.Controllers;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Repository;
using Team11_SSIS_ADProject.SSIS.Service;
using Unity;
using Unity.Injection;

namespace Team11_SSIS_ADProject
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<AccountController>(new InjectionConstructor());

            container.RegisterType<ISupplierRepository, SupplierRepository>();
            container.RegisterType<IItemCategoryRepository, ItemCategoryRepository>();
            container.RegisterType<IDepartmentRepository, DepartmentRepository>();

            container.RegisterType<ISupplierService, SupplierService>();
            container.RegisterType<IItemCategoryService, ItemCategoryService>();
            container.RegisterType<IDepartmentService, DepartmentService>();

            container.RegisterType<IItemRepository, ItemRepository>();
            container.RegisterType<IInventoryRepository, InventoryRepository>();
            container.RegisterType<IStockAdjustmentRepository, StockAdjustmentRepository>();
            container.RegisterType<IItemStockAdjustmentRepository, ItemStockAdjustmentRepository>();
            container.RegisterType<IItemService, ItemService>();
            container.RegisterType<IStockAdjustmentService, StockAdjustmentService>();

            container.RegisterType<IRequisitionRepository, RequisitionRepository>();
            container.RegisterType<IRequisitionService, RequisitionRepostiory>();

            container.RegisterType<IItemRequisitionRepository, ItemRequisitionRepository>();
            container.RegisterType<IItemRequisitionService, ItemRequisitionService>();

            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
        }
    }
}