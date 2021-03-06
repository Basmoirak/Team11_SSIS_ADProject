﻿using System;
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
using System.Web.UI.WebControls;
using System.IO;

namespace Team11_SSIS_ADProject.Controllers
{
    public class NotificationController : Controller
    {
        INotificationService notificationService;
        IItemService itemService;

        public NotificationController(INotificationService notificationService,IItemService itemService)
        {
            this.notificationService = notificationService;
            this.itemService = itemService;
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



        //Send 
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
        [HttpPost]
        public ActionResult notify_when_save(Requisition requisition)
        {
            Notification notification_ToHead = new Notification
            {
                To = "e0457779@u.nus.edu", //change to real email address whent test
                Subject = "New Requisition",
                From = User.Identity.Name,
                Body = "Requisition Detail"
            };
            notificationService.Save(notification_ToHead);
            MailMessage mm = new MailMessage();
            mm.To.Add(new MailAddress(notification_ToHead.To));
            mm.Subject = notification_ToHead.Subject;
            mm.From = new MailAddress(notification_ToHead.From);
            mm.Body = EmailBody(requisition);
            SmtpClient client = new SmtpClient();
            mm.IsBodyHtml = true;
            client.Send(mm);

            return Json(new { subject= notification_ToHead.Subject });
        }

        public ActionResult Delete(string id)
        {
            System.Uri uri = System.Web.HttpContext.Current.Request.UrlReferrer;
            notificationService.Delete(id);

            return Redirect(uri.ToString());
        }

        public String EmailBody(Requisition requisition)
        {
            String body = String.Empty; 
            var s=new List<string>();
            int startindex = 0;
            String table = null;
            using(StreamReader reader=new StreamReader(Server.MapPath("~/Views/HtmlPage1.html")))
            {
                body = reader.ReadToEnd();
                
            }
            int num = requisition.ItemRequisitions.Count();
            String[] tableline = new string[num];
            for (int i = 0; i < num; i++)
            {
                tableline[i] = "<tr></tr>";
            }
            foreach (var item in requisition.ItemRequisitions)
            {
                String itemname = itemService.Get(item.ItemId).ItemDescription;
                tableline[startindex]=tableline[startindex].Insert(4, "<td>"+itemname+"</td>"+ "<td>" + item.Quantity + "</td>");
                startindex++;
            }
            foreach (var line in tableline)
            {
                table += line;
            }
            body = body.Replace("{sss}", table);
            return body;
        }

    }
}