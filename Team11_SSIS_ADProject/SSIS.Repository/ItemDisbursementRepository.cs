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

        public IQueryable<GroupedItemDisbursements> groupItemDisbursementByDepartment()
        {
            var grouped = _context.Disbursements
                .Include("Departments")
                .Include("ItemDisbursements")
                .Where(x => x.Status == CustomStatus.ForRetrieval)
                .GroupBy(x => x.Department.DepartmentName)
                .Select(group => new GroupedItemDisbursements
                {
                    DepartmentName = group.Key,
                    ItemDisbursements = group.SelectMany(x => x.ItemDisbursements).ToList()
                });

            return grouped;
        }
    }
}