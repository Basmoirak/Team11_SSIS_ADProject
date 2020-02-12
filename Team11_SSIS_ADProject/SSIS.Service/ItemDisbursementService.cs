using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts.Repositories;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.SSIS.Service
{
    public class ItemDisbursementService : IItemDisbursementService
    {
        IItemDisbursementRepository itemDisbursementContext;

        public ItemDisbursementService(IItemDisbursementRepository itemDisbursementRepository)
        {
            this.itemDisbursementContext = itemDisbursementRepository;
        }
        public void Delete(string Id)
        {
            var itemDisbursement = itemDisbursementContext.Get(Id);
            itemDisbursementContext.Remove(itemDisbursement);
            itemDisbursementContext.Commit();
        }

        public ItemDisbursement Get(string id)
        {
            return itemDisbursementContext.Get(id);
        }

        public IEnumerable<ItemDisbursement> GetAll()
        {
            return itemDisbursementContext.GetAll();
        }

        public void Save(ItemDisbursement itemDisbursement)
        {
            ItemDisbursement id = itemDisbursementContext.Get(itemDisbursement.Id);
            if(id == null)
            {
                itemDisbursementContext.Add(itemDisbursement);
            }
            else
            {
                id.ItemId = itemDisbursement.ItemId;
                id.DisbursementId = itemDisbursement.DisbursementId;
                id.RequestedQuantity = itemDisbursement.RequestedQuantity;
                id.AvailableQuantity = itemDisbursement.AvailableQuantity;
                id.Remark = itemDisbursement.Remark;
                id.Status = itemDisbursement.Status;
            }

            itemDisbursementContext.Commit();
        }

        public IEnumerable<GroupedDepartmentCollections> GetDepartmentCollection(string departmentId)
        {
            return itemDisbursementContext.GetDepartmentCollection(departmentId);
        }

        public IEnumerable<GroupedDepartmentCollections> groupItemDisbursementByDepartment()
        {
            return itemDisbursementContext.groupItemDisbursementByDepartment();
        }

        public IEnumerable<MobileGroupedDepartmentCollections> groupItemDisbursementByDepartmentMobile()
        {
            return itemDisbursementContext.groupItemDisbursementByDepartmentMobile();
        }

        public IEnumerable<GroupedItemID> groupItemDisbursementByItemID()
        {
            return itemDisbursementContext.groupItemDisbursementByItemID();
        }

        public IEnumerable<GroupedItemID> groupItemDisbursementsByDateRange(DateTime startDate, DateTime endDate)
        {
            return itemDisbursementContext.groupItemDisbursementsByDateRange(startDate, endDate);
        }
    }
}