using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts.Repositories;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.SSIS.Repository
{
    public class ItemDisbursementRepository : Repository<ItemDisbursement>, IItemDisbursementRepository
    {
        public IEnumerable<GroupedItemID> groupItemDisbursementByItemID()
        {
            var grouped = _context.Disbursements
                .Include("ItemDisbursements")
                .Include("Departments")
                .Include("Items")
                .Where(x => x.Status == CustomStatus.ForRetrieval)
                .SelectMany(x => x.ItemDisbursements)
                .GroupBy(x => x.Item)
                .Select(group => new GroupedItemID
                {
                    ItemID = group.Key.Id,
                    ItemCode = group.Key.ItemNumber,
                    ItemDescription = group.Key.ItemDescription,
                    AvailableQuantity = group.Sum(x => x.AvailableQuantity),
                    RequestedQuantity = group.Sum(x => x.RequestedQuantity)
                }).ToList();

            return grouped;
        }

        public IEnumerable<GroupedDepartmentCollections> groupItemDisbursementByDepartment()
        {
            var grouped = _context.Disbursements
                .Include("Departments")
                .Include("ItemDisbursements")
                .Include("Items")
                .Where(x => x.Status == CustomStatus.ReadyForCollection)
                .GroupBy(x => x.Department)
                .Select(group => new GroupedDepartmentCollections
                {
                    DepartmentName = group.Key.DepartmentName,
                    CollectionPoint = group.Key.DepartmentCollectionPoint,
                    ItemDisbursements = group.SelectMany(x => x.ItemDisbursements)
                                             .GroupBy(x => x.Item)
                                             .Select(collection => new GroupedItemCollection
                                             {
                                                 ItemID = collection.Key.Id,
                                                 ItemCode = collection.Key.ItemNumber,
                                                 ItemDescription = collection.Key.ItemDescription,
                                                 AvailableQuantity = collection.Sum(x => x.AvailableQuantity),
                                                 RequestedQuantity = collection.Sum(x => x.RequestedQuantity)
                                             }).ToList()
                }).ToList();

            return grouped;
        }

        public IEnumerable<GroupedDepartmentCollections> GetDepartmentCollection(string departmentId)
        {
            var grouped = _context.Disbursements
                .Include("Departments")
                .Include("ItemDisbursements")
                .Include("Items")
                .Where(x => x.Status == CustomStatus.ReadyForCollection)
                .Where(x => x.DepartmentId == departmentId)
                .GroupBy(x => x.Department)
                .Select(group => new GroupedDepartmentCollections
                {
                    DepartmentName = group.Key.DepartmentName,
                    CollectionPoint = group.Key.DepartmentCollectionPoint,
                    ItemDisbursements = group.SelectMany(x => x.ItemDisbursements)
                                             .GroupBy(x => x.Item)
                                             .Select(collection => new GroupedItemCollection
                                             {
                                                 ItemID = collection.Key.Id,
                                                 ItemCode = collection.Key.ItemNumber,
                                                 ItemDescription = collection.Key.ItemDescription,
                                                 AvailableQuantity = collection.Sum(x => x.AvailableQuantity),
                                                 RequestedQuantity = collection.Sum(x => x.RequestedQuantity)
                                             }).ToList()
                }).ToList();

            return grouped;
        }

        public IEnumerable<MobileGroupedDepartmentCollections> groupItemDisbursementByDepartmentMobile()
        {
            var grouped = _context.Disbursements
                .Include("Departments")
                .Include("ItemDisbursements")
                .Include("Items")
                .Where(x => x.Status == CustomStatus.ReadyForCollection)
                .GroupBy(x => x.Department)
                .Select(group => new MobileGroupedDepartmentCollections
                {
                    DepartmentId = group.Key.Id,
                    DepartmentName = group.Key.DepartmentName,
                    CollectionPoint = group.Key.DepartmentCollectionPoint,
                    ItemDisbursements = group.SelectMany(x => x.ItemDisbursements)
                                             .GroupBy(x => x.Item)
                                             .Select(collection => new GroupedItemCollection
                                             {
                                                 ItemID = collection.Key.Id,
                                                 ItemCode = collection.Key.ItemNumber,
                                                 ItemDescription = collection.Key.ItemDescription,
                                                 AvailableQuantity = collection.Sum(x => x.AvailableQuantity),
                                                 RequestedQuantity = collection.Sum(x => x.RequestedQuantity)
                                             }).ToList()
                }).ToList();

            return grouped;
        }
    }
}