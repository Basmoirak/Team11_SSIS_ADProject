using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.Repository;

namespace Team11_SSIS_ADProject.SSIS.Service
{
    public class NotificationService : INotificationService
    {
        INotificationRepository notificationContext;

        public NotificationService(INotificationRepository notificationRepository)
        {
            this.notificationContext = notificationRepository;
        }

        public void Delete(string Id)
        {
            var notification = notificationContext.Get(Id);
            notificationContext.Remove(notification);
            notificationContext.Commit();
        }

        public Notification Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Notification> GetAll()
        {

            return notificationContext.GetAll();
        }

        public void Save(Notification notification)
        {
            notificationContext.Add(notification);
            notificationContext.Commit();
        }
    }
}