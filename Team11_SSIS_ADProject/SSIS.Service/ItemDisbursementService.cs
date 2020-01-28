﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts.Repositories;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models;

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
                id.Quantity = itemDisbursement.Quantity;
                id.Remark = itemDisbursement.Remark;
            }

            itemDisbursementContext.Commit();
        }
    }
}