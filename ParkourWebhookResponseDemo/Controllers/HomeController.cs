using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ParkourWebhookResponseDemo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        public ActionResult Index()
        {
            return View();
        }   
        [HttpPost]
        public ActionResult StripeWebhook()
        {

            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);

            string json = new StreamReader(req).ReadToEnd();

            //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
            //{
            //    EnableSsl = true
            //};
            var Username = "ycaresite@gmail.com";
            var Password = "YCare007";

            var ToAddress = "salman@differenzsystem.com";
            
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(Username);
            mailMessage.To.Add(ToAddress);
            mailMessage.Subject = "Parkour Stripe Webhook";
            mailMessage.Body = json;

            mailMessage.IsBodyHtml = true;
            var smtpClient = new SmtpClient { EnableSsl = true };
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(Username, Password);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(mailMessage);
            //IsMailSent = true;

            //smtpClient.Credentials = new System.Net.NetworkCredential("ycaresite@gmail.com", "YCare007");
            //smtpClient.UseDefaultCredentials = true;
            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtpClient.EnableSsl = true;
            //MailMessage mail = new MailMessage();
            //mail.Body = json;
            ////Setting From , To and CC
            //mail.From = new MailAddress("ycaresite@gmail.com", "Parkour Stripe Webhook");
            //mail.To.Add(new MailAddress("salman@differenzsystem.com"));
            //smtpClient.Send(mail);
            return View();
        }
    }
}