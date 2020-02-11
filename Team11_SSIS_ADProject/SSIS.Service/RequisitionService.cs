using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Service
{
    public class RequisitionService : IRequisitionService
    {
        IRequisitionRepository requisitionContext;
        public RequisitionService(IRequisitionRepository requisitionContext)
        {
            this.requisitionContext = requisitionContext;
        }
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Requisition Get(string id)
        {
            return requisitionContext.Get(id);
        }

        public IEnumerable<Requisition> GetAll()
        {
            return requisitionContext.GetAll();
        }

        public void Save(Requisition requisition)
        {
            Requisition r = requisitionContext.Get(requisition.Id);
            if(r == null)
            {
                requisitionContext.Add(requisition);
            }
            else
            {
                r.Remark = requisition.Remark;
                r.Status = requisition.Status;
             
            }
            
            requisitionContext.Commit();
        }

        public IEnumerable<Requisition> getAllPendingRequisitionsByDepartment(string departmentId)
        {
            return requisitionContext.getAllPendingRequisitionsByDepartment(departmentId);
        }
    }
}