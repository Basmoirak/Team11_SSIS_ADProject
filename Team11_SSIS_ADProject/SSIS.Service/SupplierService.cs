    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.Repository;

namespace Team11_SSIS_ADProject.SSIS.Service
{
    public class SupplierService : ISupplierService
    {
        public ISupplierRepository supplierContext;
        public SupplierService(ISupplierRepository supplierRepository)
        {
            this.supplierContext = supplierRepository;
        }

        public void Delete(string Id)
        {
            var supplier = supplierContext.Get(Id);
            supplierContext.Remove(supplier);
            supplierContext.Commit();
        }

        public Supplier Get(string id)
        {
            return supplierContext.Get(id);
        }

        public IEnumerable<Supplier> GetAll()
        {
            return supplierContext.GetAll();
        }

        public void Save(Supplier supplier)
        {
            Supplier s = supplierContext.Get(supplier.Id);
            if (s == null)
            {
                supplierContext.Add(supplier);
            }
            else
            {
                s.SupplierName = supplier.SupplierName;
                s.SupplierPhone = supplier.SupplierPhone;
                s.SupplierGSTNumber = supplier.SupplierGSTNumber;
                s.SupplierFax = supplier.SupplierFax;
                s.SupplierContactName = supplier.SupplierContactName;
                s.SupplierAddress = supplier.SupplierAddress;
                s.SupplierCode = supplier.SupplierCode;
            }
            supplierContext.Commit();
        }
    }
}