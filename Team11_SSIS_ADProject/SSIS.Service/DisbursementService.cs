using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts.Repositories;
using Team11_SSIS_ADProject.SSIS.Contracts.Services;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Service
{
    public class DisbursementService : IDisbursementService
    {
        IDisbursementRepository disbursementContext;

        public DisbursementService(IDisbursementRepository disbursementRepository)
        {
            this.disbursementContext = disbursementRepository;
        }

        public void Delete(string Id)
        {
            var disbursement = disbursementContext.Get(Id);
            disbursementContext.Remove(disbursement);
            disbursementContext.Commit();
        }

        public Disbursement Get(string id)
        {
            return disbursementContext.Get(id);
        }

        public IEnumerable<Disbursement> GetAll()
        {
            return disbursementContext.GetAll();
        }

        public IEnumerable<Disbursement> getAllDisbursementsByStatusAndDepartmentId(int status, string departmentId)
        {
            return disbursementContext.getAllDisbursementsByStatusAndDepartmentId(status, departmentId);
        }

        public IEnumerable<ItemDisbursement> getAllItemDisbursementsByStatus(int status)
        {
            return disbursementContext.getAllItemDisbursementsByStatus(status);
        }

        public void Save(Disbursement disbursement)
        {
            Disbursement d = disbursementContext.Get(disbursement.Id);
            if (d == null)
            {
                disbursementContext.Add(disbursement);
            }
            else
            {
                d.Remarks = disbursement.Remarks;
                d.Status = disbursement.Status;
                d.DepartmentId = disbursement.DepartmentId;
            }

            disbursementContext.Commit();
        }
    }
}