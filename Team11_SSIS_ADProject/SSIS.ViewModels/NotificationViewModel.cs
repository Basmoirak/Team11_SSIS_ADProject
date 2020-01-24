using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.ViewModels
{
    public class NotificationViewModel
    {
        public Notification Notification{ get; set; }
        public List<Notification> Notifications { get; set; }
    }
}