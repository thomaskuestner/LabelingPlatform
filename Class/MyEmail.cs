/*
k-Space Astronauts labeling platform
    (c) 2016 under Apache 2.0 license 
    Thomas Kuestner, Martin Schwartz, Philip Wolf
    Please refer to https://sites.google.com/site/kspaceastronauts/iqa/labelingplatform for more information
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Net.Mail;
using LabelingFramework.Utility;

//*********************************************************************************************************//
//  class for email handling
//*********************************************************************************************************//

namespace LabelingFramework.Class
{
    public class MyEmail
    {
        public string sender = Constant.adminEmailAddress;
        private string serverName = Constant.adminEmailServer;
        private int port = Constant.adminEmailPort;
        private string userName = Constant.adminEmailAddress;
        private string password = Constant.adminEmailPW;
        public MailMessage email;
        private SmtpClient MailClient;

        public MyEmail() {
            email = new MailMessage();
            email.From = new MailAddress(sender);
            MailClient = new SmtpClient(serverName, port);
            // MailClient.EnableSsl = true;
            MailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailClient.Credentials = new System.Net.NetworkCredential(userName, password);
        }


        public void sendEmail(string recipient, string subject, string message)
        {
            email.To.Add(recipient);
            email.Subject = subject;
            email.Body = message;
			email.IsBodyHtml = true;
            MailClient.Send(email);
        }

        public void sendEmail(string recipient, string subject, string message, bool attachFile)
        {
            email.To.Add(recipient);
            email.Subject = subject;
            email.Body = message;
			email.IsBodyHtml = true;

            if ((Constant.attachment != null) && attachFile)
            {
                email.Attachments.Add(new Attachment(Constant.attachment));
            }

            MailClient.Send(email);
        }

        


        public void sendEmailToAdmin(string subject, string message)
        {
            email.To.Add(sender);
            email.Subject = subject;
            email.Body = message;
            MailClient.Send(email);
        }


    }
}