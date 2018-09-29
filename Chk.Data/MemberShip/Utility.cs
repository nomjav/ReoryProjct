using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

using System.Net.Mime;
using System.Web.Hosting;
using System.Threading.Tasks;
using Chakwal.Data.Repository;
using Chakwal.Data.Data;

namespace AcademyLockSmith.Data.MemberShip
{
    public class Utility
    {
        //private static readonly UnitOfWork unitOfWork = new UnitOfWork();

        public static bool UserNameAvailable(string userName)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            List<User> users = unitOfWork.UserRepository.Get(x => x.UserName.Equals(userName));
            if (!users.Any())
            {
                return true;
            }
            return false;
        }

        public static bool EmailAvailable(string userEmail)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            List<User> users = unitOfWork.UserRepository.Get(x => x.Email.Equals(userEmail));
            if (!users.Any())
            {
                return true;
            }
            return false;
        }

        //public async Task SendEmailAsync(string subject, string message, MailAddress toAddress, string filePath, bool isLateLetter = false, bool isCustomerUpdate = false, List<string> ccList = null, List<string> bccList = null)
        //{
        //    try
        //    {
        //        UnitOfWork unitOfWork = new UnitOfWork();
        //        SP_GetSMTPDetails_Result smtpSettings =
        //            unitOfWork.SmtpRepository.context.Database.SqlQuery<SP_GetSMTPDetails_Result>("EXEC SP_GetSMTPDetails")
        //                .FirstOrDefault();
        //        if (smtpSettings != null)
        //        {
        //            using (var message1 = new MailMessage())
        //            {
        //                message1.From = new MailAddress(smtpSettings.AdminEmail);
        //                message1.To.Add(toAddress);

        //                if ((ccList != null))
        //                {
        //                    foreach (var email in ccList)
        //                    {
        //                        if (email != "")
        //                            message1.CC.Add(new MailAddress(email));
        //                    }
        //                }
        //                if ((bccList != null))
        //                {
        //                    foreach (var email in bccList)
        //                    {
        //                        if (email != "")
        //                            message1.Bcc.Add(new MailAddress(email));
        //                    }
        //                }

        //                var bccSettingAccount = unitOfWork.SettingRepository.GetAsQuerable(t => t.Name == "BCCEmailAccount").Select(t => t.Value).FirstOrDefault();
        //                if (!String.IsNullOrEmpty(bccSettingAccount))
        //                {
        //                    if (bccSettingAccount.Contains(','))
        //                    {
        //                        foreach (String emailAddress in bccSettingAccount.Split(','))
        //                        {
        //                            if (!String.IsNullOrEmpty(emailAddress))
        //                                message1.Bcc.Add(new MailAddress(emailAddress));
        //                        }
        //                    }
        //                    else
        //                        message1.Bcc.Add(new MailAddress(bccSettingAccount));
        //                }

        //                message1.Subject = subject;
        //                message1.Body = message;
        //                message1.IsBodyHtml = true;
        //                if (isLateLetter == true)
        //                {
        //                    message1.ReplyToList.Add(smtpSettings.ReplyToLateLetter);
        //                }
        //                else if (isCustomerUpdate == true)
        //                {
        //                    smtpSettings.AdminEmail = unitOfWork.SettingRepository.GetAsQuerable(t => t.Code == "UpdateCustomerAdminEmail").FirstOrDefault().Value;
        //                    smtpSettings.AdminPassword = unitOfWork.SettingRepository.GetAsQuerable(t => t.Code == "UpdateCustomerAdminPassword").FirstOrDefault().Value;
        //                    message1.ReplyToList.Add(smtpSettings.ReplyToCustomerUpdate);
        //                }
        //                else
        //                {
        //                    message1.ReplyToList.Add(smtpSettings.ReplyTo);
        //                }

        //                if (!string.IsNullOrEmpty(filePath))
        //                {
        //                    Attachment attach = null;
        //                    if (HttpContext.Current != null)
        //                        attach = new Attachment(HttpContext.Current.Server.MapPath(filePath));
        //                    else
        //                        attach = new Attachment(HostingEnvironment.MapPath(filePath));
        //                    message1.Attachments.Add(attach);
        //                }

        //                var smtpClient = new SmtpClient();
        //                smtpClient.Host = smtpSettings.SMPTServer;
        //                smtpClient.Port = Convert.ToInt32(smtpSettings.Port);
        //                smtpClient.EnableSsl = (bool)smtpSettings.SSL;
        //                smtpClient.Credentials = new NetworkCredential(smtpSettings.AdminEmail, smtpSettings.AdminPassword);
        //                smtpClient.Timeout = 10000000;
        //                try
        //                {
        //                    await smtpClient.SendMailAsync(message1);
        //                }
        //                catch (SmtpException ex)
        //                {
        //                    if (ex.InnerException.Message.Equals("Unable to read data from the transport connection: An existing connection was forcibly closed by the remote host."))
        //                        smtpClient.Send(message1);
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception exception)
        //    {
        //        Logger.Logger.LogException("Exception thrown in Method Send Mail: " + exception.Message, exception);
        //    }
        //}

        //public static void SendEmail(string subject, string message, MailAddress toAddress, string filePath, bool isLateLetter = false, bool isCustomerUpdate = false, List<string> ccList = null, List<string> bccList = null)
        //{
        //    try
        //    {
        //        UnitOfWork unitOfWork = new UnitOfWork();
        //        SP_GetSMTPDetails_Result smtpSettings =
        //            unitOfWork.SmtpRepository.context.Database.SqlQuery<SP_GetSMTPDetails_Result>("EXEC SP_GetSMTPDetails")
        //                .FirstOrDefault();
        //        if (smtpSettings != null)
        //        {
        //            using (var message1 = new MailMessage())
        //            {
        //                message1.From = new MailAddress(smtpSettings.AdminEmail);
        //                //var message1 = new MailMessage { From = new MailAddress(smtpSettings.AdminEmail) };
        //                message1.To.Add(toAddress);

        //                if ((ccList != null))
        //                {
        //                    foreach (var email in ccList)
        //                    {
        //                        if (email != "")
        //                            message1.CC.Add(new MailAddress(email));
        //                    }
        //                }
        //                if ((bccList != null))
        //                {
        //                    foreach (var email in bccList)
        //                    {
        //                        if (email != "")
        //                            message1.Bcc.Add(new MailAddress(email));
        //                    }
        //                }

        //                var bccSettingAccount = unitOfWork.SettingRepository.GetAsQuerable(t => t.Name == "BCCEmailAccount").Select(t => t.Value).FirstOrDefault();
        //                if (!String.IsNullOrEmpty(bccSettingAccount))
        //                {
        //                    if (bccSettingAccount.Contains(','))
        //                    {
        //                        foreach (String emailAddress in bccSettingAccount.Split(','))
        //                        {
        //                            if (!String.IsNullOrEmpty(emailAddress))
        //                                message1.Bcc.Add(new MailAddress(emailAddress));
        //                        }
        //                    }
        //                    else
        //                        message1.Bcc.Add(new MailAddress(bccSettingAccount));
        //                }

        //                message1.Subject = subject;
        //                message1.Body = message;
        //                message1.IsBodyHtml = true;
        //                if (isLateLetter == true)
        //                {
        //                    message1.ReplyToList.Add(smtpSettings.ReplyToLateLetter);
        //                }
        //                else if (isCustomerUpdate == true)
        //                {
        //                    smtpSettings.AdminEmail = unitOfWork.SettingRepository.GetAsQuerable(t => t.Code == "UpdateCustomerAdminEmail").FirstOrDefault().Value;
        //                    smtpSettings.AdminPassword = unitOfWork.SettingRepository.GetAsQuerable(t => t.Code == "UpdateCustomerAdminPassword").FirstOrDefault().Value;
        //                    message1.ReplyToList.Add(smtpSettings.ReplyToCustomerUpdate);
        //                }

        //                else
        //                {
        //                    message1.ReplyToList.Add(smtpSettings.ReplyTo);
        //                }
        //                if (!string.IsNullOrEmpty(filePath))
        //                {
        //                    Attachment attach = null;
        //                    if (HttpContext.Current != null)
        //                        attach = new Attachment(HttpContext.Current.Server.MapPath(filePath));
        //                    else
        //                        attach = new Attachment(HostingEnvironment.MapPath(filePath));
                            
        //                    message1.Attachments.Add(attach);
        //                }
        //                var smtpClient = new SmtpClient();
        //                smtpClient.Host = smtpSettings.SMPTServer;
        //                smtpClient.Port = Convert.ToInt32(smtpSettings.Port);
        //                smtpClient.EnableSsl = (bool)smtpSettings.SSL;
        //                smtpClient.Credentials = new NetworkCredential(smtpSettings.AdminEmail, smtpSettings.AdminPassword);
        //                smtpClient.Timeout = 10000000;
        //                try
        //                {
        //                    smtpClient.Send(message1);
        //                }
        //                catch (SmtpException ex)
        //                {
        //                    if (ex.InnerException.Message.Equals("Unable to read data from the transport connection: An existing connection was forcibly closed by the remote host."))
        //                        smtpClient.Send(message1);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        Logger.Logger.LogException("Exception thrown in Method Send Mail: " + exception.Message, exception);
        //    }
        //}

        //public static void SendEmailMultipleAttatchments(string subject, string message, MailAddress toAddress, List<string> filesPathList, bool isLateLetter = false, bool isCustomerUpdate = false, List<string> ccList = null, List<string> bccList = null)
        //{
        //    try
        //    {
        //        UnitOfWork unitOfWork = new UnitOfWork();
        //        SP_GetSMTPDetails_Result smtpSettings =
        //            unitOfWork.SmtpRepository.context.Database.SqlQuery<SP_GetSMTPDetails_Result>("EXEC SP_GetSMTPDetails")
        //                .FirstOrDefault();
        //        if (smtpSettings != null)
        //        {
        //            var message1 = new MailMessage { From = new MailAddress(smtpSettings.AdminEmail) };
        //            message1.To.Add(toAddress);

        //            if ((ccList != null))
        //            {
        //                foreach (var email in ccList)
        //                {
        //                    if (email != "")
        //                        message1.CC.Add(new MailAddress(email));
        //                }
        //            }
        //            if ((bccList != null))
        //            {
        //                foreach (var email in bccList)
        //                {
        //                    if (email != "")
        //                        message1.Bcc.Add(new MailAddress(email));
        //                }
        //            }

        //            var bccSettingAccount = unitOfWork.SettingRepository.GetAsQuerable(t => t.Name == "BCCEmailAccount").Select(t => t.Value).FirstOrDefault();
        //            if (!String.IsNullOrEmpty(bccSettingAccount))
        //            {
        //                if (bccSettingAccount.Contains(','))
        //                {
        //                    foreach (String emailAddress in bccSettingAccount.Split(','))
        //                    {
        //                        if (!String.IsNullOrEmpty(emailAddress))
        //                            message1.Bcc.Add(new MailAddress(emailAddress));
        //                    }
        //                }
        //                else
        //                    message1.Bcc.Add(new MailAddress(bccSettingAccount));
        //            }

        //            message1.Subject = subject;
        //            message1.Body = message;
        //            message1.IsBodyHtml = true;
        //            if (isLateLetter == true)
        //            {
        //                message1.ReplyToList.Add(smtpSettings.ReplyToLateLetter);
        //            }
        //            else if (isCustomerUpdate == true)
        //            {
        //                message1.ReplyToList.Add(smtpSettings.ReplyToCustomerUpdate);
        //            }

        //            else
        //            {
        //                message1.ReplyToList.Add(smtpSettings.ReplyTo);
        //            }
        //            if (filesPathList != null)
        //            {
        //                foreach (var filePath in filesPathList)
        //                {
        //                    var attach = new Attachment(HttpContext.Current.Server.MapPath(filePath));
        //                    message1.Attachments.Add(attach);
        //                }
        //            }
        //            var smtpClient = new SmtpClient
        //            {
        //                Host = smtpSettings.SMPTServer,
        //                Port = Convert.ToInt32(smtpSettings.Port),
        //                EnableSsl = (bool)smtpSettings.SSL,
        //                Credentials = new NetworkCredential(smtpSettings.AdminEmail,
        //                    smtpSettings.AdminPassword),
        //                Timeout = 10000000
        //            };
        //            try
        //            {
        //                smtpClient.Send(message1);
        //            }
        //            catch (System.Net.Mail.SmtpException ex)
        //            {
        //                if (ex.InnerException.Message.Equals("Unable to read data from the transport connection: An existing connection was forcibly closed by the remote host."))
        //                    smtpClient.Send(message1);
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        Logger.Logger.LogException("Exception thrown in Method Send Mail: " + exception.Message, exception.StackTrace);
        //    }
        //}
        //public static bool SendEmail(string subject, AlternateView av, MailAddress toAddress, string filePath, bool isLateLetter = false)
        //{
        //    try
        //    {
        //        UnitOfWork unitOfWork = new UnitOfWork();
        //        SP_GetSMTPDetails_Result smtpSettings =
        //            unitOfWork.SmtpRepository.context.Database.SqlQuery<SP_GetSMTPDetails_Result>("EXEC SP_GetSMTPDetails")
        //                .FirstOrDefault();
        //        if (smtpSettings != null)
        //        {
        //            var message1 = new MailMessage { From = new MailAddress(smtpSettings.AdminEmail) };
        //            message1.To.Add(toAddress);
        //            message1.Subject = subject;
        //            message1.AlternateViews.Add(av);
        //            message1.IsBodyHtml = true;
        //            var bccSettingAccount = unitOfWork.SettingRepository.GetAsQuerable(t => t.Name == "BCCEmailAccount").Select(t => t.Value).FirstOrDefault();
        //            if (!String.IsNullOrEmpty(bccSettingAccount))
        //            {
        //                if (bccSettingAccount.Contains(','))
        //                {
        //                    foreach (String emailAddress in bccSettingAccount.Split(','))
        //                    {
        //                        if (!String.IsNullOrEmpty(emailAddress))
        //                            message1.Bcc.Add(new MailAddress(emailAddress));
        //                    }
        //                }
        //                else
        //                    message1.Bcc.Add(new MailAddress(bccSettingAccount));
        //            }
        //            if (isLateLetter == true)
        //            {
        //                message1.ReplyToList.Add(smtpSettings.ReplyToLateLetter);
        //            }
        //            else
        //            {
        //                message1.ReplyToList.Add(smtpSettings.ReplyTo);
        //            }
        //            if (!string.IsNullOrEmpty(filePath))
        //            {
        //                var attach = new Attachment(HttpContext.Current.Server.MapPath(filePath));
        //                message1.Attachments.Add(attach);
        //            }
        //            var smtpClient = new SmtpClient
        //            {
        //                Host = smtpSettings.SMPTServer,
        //                Port = Convert.ToInt32(smtpSettings.Port),
        //                EnableSsl = (bool)smtpSettings.SSL,
        //                Credentials = new NetworkCredential(smtpSettings.AdminEmail,
        //                    smtpSettings.AdminPassword),
        //                Timeout = 10000000
        //            };
        //            try
        //            {
        //                smtpClient.Send(message1);
        //            }
        //            catch (SmtpException ex)
        //            {
        //                if (ex.InnerException.Message.Equals("Unable to read data from the transport connection: An existing connection was forcibly closed by the remote host."))
        //                    smtpClient.Send(message1);
        //            }
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception exception)
        //    {
        //        Logger.Logger.LogException("Exception thrown in Method Send Mail: " + exception.Message, exception);
        //        return false;
        //    }
        //}

        //public static string EnsureDirectory(string path)
        //{
        //    string directoryPath = null;
        //    directoryPath = HttpContext.Current.Server.MapPath(path);
        //    if (!Directory.Exists(directoryPath))
        //    {
        //        //Trace.WriteLine(directoryPath + "Not exists, Creating new Directory", "Utility.EnsureDirectory");
        //        Directory.CreateDirectory(directoryPath);
        //    }

        //    return directoryPath;
        //}
    }
}