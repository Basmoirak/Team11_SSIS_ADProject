using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security;
using Team11_SSIS_ADProject.SSIS.Models;
using System.Net;
using System.Net.Mail;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Service;
using Team11_SSIS_ADProject.SSIS.ViewModels;
using System.Security.Claims;
using System.Web.Helpers;

namespace Team11_SSIS_ADProject.Controllers
{
    public class NotificationController : Controller
    {
        INotificationService notificationService;

        public NotificationController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        
        // GET: Notification
        public ActionResult Index()
        {
            var viewModel = new NotificationViewModel
            {
                Notifications = notificationService.GetAll().ToList()
                               .Where(n => n.To.Contains(User.Identity.Name)).OrderByDescending(n => n.createdDateTime).ToList() 

            };
            return View(viewModel);
        }
        //Get:OutBox
        public ActionResult OutBox()
        {
            var viewModel = new NotificationViewModel
            {
                Notifications = notificationService.GetAll().ToList()
                               .Where(n => n.From == User.Identity.Name).OrderByDescending(n => n.createdDateTime).ToList()

            };
            return View(viewModel);
        }
        //GET: Email-form
        public ActionResult SendNoti()
        {
            var viewModel = new NotificationViewModel { Notification=new Notification() };
            return View(viewModel);
        }

        public void sendEmail(Notification notification)
        {
            SmtpClient client = new SmtpClient();
            MailMessage mm = new MailMessage();
            mm.Subject = notification.Subject;
            mm.Body = notification.Body;
            String[] multi_emailaddress = notification.To.Split(new char[2] { ',', ';' });
            foreach (var emaddress in multi_emailaddress)
            {
                mm.To.Add(emaddress);
            }
                       
            client.Send(mm);
        }
        
        //Send Email
        [HttpPost]
        public ActionResult Notify(Notification notification)
        {
            Notification mmnotification = new Notification();           
            mmnotification.To = notification.To;
            mmnotification.Subject = notification.Subject;
            mmnotification.From = User.Identity.Name;
            mmnotification.Body = notification.Body;
            notificationService.Save(mmnotification);
            sendEmail(mmnotification);  
            
            return RedirectToAction("OutBox", "Notification");

        }

        //Send email simultaneously when submit requisition 
        public ActionResult notify_when_save(Notification notification)
        {
            Notification notification_ToHead = new Notification 
            {
                To = "1002633246@qq.com", //change to real email address whent test
                Subject = "Requisition",
                From= User.Identity.Name, 
                Body="Requisition_approve"
            };
            notificationService.Save(notification_ToHead);
            sendEmail(notification_ToHead);  

            return Json(new { subject= notification_ToHead.Subject });
        }

        public ActionResult Delete(string id)
        {
            System.Uri uri = System.Web.HttpContext.Current.Request.UrlReferrer;
            notificationService.Delete(id);

            return Redirect(uri.ToString());
        }

    }
}