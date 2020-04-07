using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


//using EASendMail; //add EASendMail namespace
namespace WPS.WF
{
    public class EmailUtil
    {
        public static string _fromAddress = "john.nate.2020@gmail.com";
        public static string _host = "smtp.gmail.com";
        public static string _login = "john.nate.2020@gmail.com";//"wishricom@gmail.com";
        public static string _password = "Matrix.3880";
        public static void SendEmail(string subject,string htmlString,string to)
        {
            
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(_fromAddress);
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.Host = _host; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_login, _password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                /*MailMessage mail = new MailMessage();
                mail.To.Add(to);
                mail.From = new MailAddress(_fromAddress);
                mail.Subject = subject;
                mail.Body = htmlString;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(_login, _password);
                smtp.Send(mail);*/

                /*var smptClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("john.nate.2020@gmail.com", "Matrix.388"),
                    EnableSsl = true
                };
                smptClient.Send("john.nate.2020@gmail.com", "wishricom@gmail.com", "Testing Email", "testing the email");
            */
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
                /*
            try
            {
                SmtpMail oMail = new SmtpMail("TryIt");

                // Your gmail email address
                oMail.From = _fromAddress;
                // Set recipient email address
                oMail.To = to;

                // Set email subject
                oMail.Subject = subject;
                // Set email body
                oMail.TextBody = htmlString;

                // Gmail SMTP server address
                SmtpServer oServer = new SmtpServer("smtp.gmail.com");

                // Gmail user authentication
                // For example: your email is "gmailid@gmail.com", then the user should be the same
                oServer.User = _login;
                oServer.Password = _password;

                // Set 465 port
                oServer.Port = 465;

                // detect SSL/TLS automatically
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                Console.WriteLine("start to send email over SSL ...");

                EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
                oSmtp.SendMail(oServer, oMail);

                Console.WriteLine("email was sent successfully!");
            }
            catch (Exception ep)
            {
                Console.WriteLine("failed to send email with the following error:");
                Console.WriteLine(ep.Message);
            }*/
        }
    }
}
