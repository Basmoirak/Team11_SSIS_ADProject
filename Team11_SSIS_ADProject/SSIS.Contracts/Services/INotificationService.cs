using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Contracts
{
    public interface INotificationService
    {
        void Save(Notification  notification);
        Notification Get(string id);
        IEnumerable<Notification> GetAll();
        void Delete(string Id);
    }
}