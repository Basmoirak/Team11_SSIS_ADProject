using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Service
{
    public class RequisitionRepostiory : IRequisitionService
    {
        IRequisitionRepository requisitionContext;
        public RequisitionRepostiory(IRequisitionRepository requisitionContext)
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
            requisitionContext.Add(requisition);
            requisitionContext.Commit();
        }
    }
}