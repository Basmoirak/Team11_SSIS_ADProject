using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Contracts.Services
{
    public interface IInventoryService
    {
        Inventory Get(string id);
        IEnumerable<Inventory> GetAll();
        void Update(Inventory inventory);
        void Save(Inventory inventory);
    }
}
