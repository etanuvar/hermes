using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Configuration;
using Netblaster.Hermes.DAL.Model;
using Netblaster.Hermes.DAL.Model.Enums;

namespace Netblaster.Hermes.WebUI.Helpers
{
    public static class EmailHelper
    {
        public static bool SendEmailToGroup(MailMessage message, Group group)
        {
            try
            {
                foreach (var usersGroups in group.UserGroups)
                {
                    try
                    {
                        message.To.Add(new MailAddress(usersGroups.ApplicationUser.Email));
                        message.From = new MailAddress("crm@netblaster.pl", "Hermes CRM");  // replace with valid value
                        using (var smtp = new SmtpClient())
                        {
                            var credential = new NetworkCredential
                            {
                                UserName = "crm@netblaster.pl",  // replace with valid value
                                Password = "Kaleka_321"  // replace with valid value
                            };
                            smtp.Credentials = credential;
                            smtp.Host = "serwer1677873.home.pl";
                            smtp.Send(message);
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool SendEmailToUsers(MailMessage message, IEnumerable<ApplicationUser> users)
        {
            try
            {
                foreach (var user in users)
                {
                    try
                    {
                        message.To.Add(new MailAddress(user.Email));
                        message.From = new MailAddress("crm@netblaster.pl", "Hermes CRM");  // replace with valid value
                        using (var smtp = new SmtpClient())
                        {
                            var credential = new NetworkCredential
                            {
                                UserName = "crm@netblaster.pl",  // replace with valid value
                                Password = "Kaleka_321"  // replace with valid value
                            };
                            smtp.Credentials = credential;
                            smtp.Host = "serwer1677873.home.pl";
                            smtp.Send(message);
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static MailMessage GenerateNewTaskMessage(string request, string filePath, ApplicationUser sender, TaskItem task)
        {
            var mailMessage = new MailMessage();

            if (File.Exists(filePath))
            {
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.Body = File.ReadAllText(filePath, Encoding.UTF8);
                mailMessage.Body = mailMessage.Body.Replace("#TITLE#", "Do Twojej grupy dodano nowe zadanie");
                mailMessage.Body = mailMessage.Body.Replace("#BODY#",
                    $"{sender.DisplayName} utworzył nowe zadanie - {task.Title} z datą wykonania: {task.DeadlineDate}.");
                mailMessage.Body = mailMessage.Body.Replace("#URL-HREF#", $"{request}/Task/Details/{task.Id}");
                mailMessage.Body = mailMessage.Body.Replace("#URL-TEXT#", $"Przejdź do zadania");
                
                mailMessage.Subject = "Do Twojej grupy dodano nowe zadanie.";
            }
            return mailMessage;
        }

        public static MailMessage GenerateNewUserMessage(string request, string filePath, ApplicationUser sender, ApplicationUser user)
        {
            var mailMessage = new MailMessage();

            if (File.Exists(filePath))
            {
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.Body = File.ReadAllText(filePath, Encoding.UTF8);
                mailMessage.Body = mailMessage.Body.Replace("#TITLE#", "Nowy użytkownik oczekuje na aktywację");
                mailMessage.Body = mailMessage.Body.Replace("#BODY#",
                    $"{user.DisplayName} dokonał rejestracji konta. Aby użytkownik mógł korzystać z aplikacji, jego konto musi zostać aktywowane.");
                mailMessage.Body = mailMessage.Body.Replace("#URL-HREF#", $"{request}/Management/Users");
                mailMessage.Body = mailMessage.Body.Replace("#URL-TEXT#", $"Przejdź do aplikacji");

                mailMessage.Subject = "Nowy użytkownik oczekuje na aktywację.";
            }
            return mailMessage;
        }
    }
}