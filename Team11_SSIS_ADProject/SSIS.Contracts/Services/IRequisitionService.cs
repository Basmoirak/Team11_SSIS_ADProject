using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Contracts
{
    public interface IRequisitionService
    {
        void Save(Requisition requisition);
        Requisition Get(string id);
        IEnumerable<Requisition> GetAll();
        void Delete(string id);
        IEnumerable<Requisition> getAllPendingRequisitionsByDepartment(string departmentId);
    }
}
