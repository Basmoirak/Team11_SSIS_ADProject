using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts.Repositories;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Repository
{
    public class DisbursementRepository: Repository<Disbursement>, IDisbursementRepository
    {
        public IEnumerable<ItemDisbursement> getAllItemDisbursementsByStatus(int status)
        {
            return _context.Disbursements
                .Include("ItemDisbursements")
                .Where(x => x.Status == status)
                .SelectMany(x => x.ItemDisbursements).ToList();
        }

        public IEnumerable<Disbursement> getAllDisbursementsByStatusAndDepartmentId(int status, string departmentId)
        {
            return _context.Disbursements
                .Include("ItemDisbursements")
                .Where(x => x.Status == status)
                .Where(x => x.DepartmentId == departmentId);
        }
    }
}