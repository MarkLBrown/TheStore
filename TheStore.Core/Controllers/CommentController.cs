using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TheStore.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.Mvc;

namespace TheStore.Core.Controllers
{
    public class CommentController:SurfaceController
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(CommentModel model)
        {
            if (ModelState.IsValid)
            {
                SendEmail(model);
                TempData["CommentSuccess"] = true;
                return RedirectToCurrentUmbracoPage();
            }
            return CurrentUmbracoPage();
        }
        private void SendEmail(CommentModel model)
        {
            MailMessage message = new MailMessage(model.Email, "website@installumbraco.web.local");
            message.Subject = string.Format("Enquiry from {0} {1} - {2}", model.Name, model.Email, model.Comment);
            message.Body = model.Comment;
            SmtpClient client = new SmtpClient("127.0.0.1", 25);
            client.Send(message);
        }

    }
}
