using AcademyLockSmith.Data.MemberShip;

using AcademyLockSmith.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.Globalization;
using System.Web.Hosting;
using System.Data.Entity.Validation;
using Chakwal.Data.Repository;
using Chakwal.Data.Data;

namespace Chakwal.Data.Helpers.EmailHelper
{
    public class EmailHelper
    {

        public string sendForgottenEmailToUser(string email, string id = "")
        {
            UnitOfWork _unitOfWork = new UnitOfWork();
            try
            {
                var user = new User();
                if (!String.IsNullOrEmpty(id))
                {
                    var usrId = Convert.ToInt32(id);
                    user = _unitOfWork.UserRepository.GetSingle(x => x.UserId == usrId && x.Email.Equals(email));
                }
                else
                {
                    user = _unitOfWork.UserRepository.GetSingle(x => x.Email.Equals(email));
                }

                if (user == null)
                {
                    return "1";
                }

                var password = user.Password;
                if (SecurityHelper.IsEncrypted(password))
                {
                    password = SecurityHelper.Decrypt(password);
                }


                string emailBody = "Dear " + user.FirstName + " " + user.LastName + ",<br /> Your password is: " + password + "<br />";
                const string subject = "ALS Password Reminder";
                if (!String.IsNullOrEmpty(email))
                {
                    //Utility.SendEmail(subject, emailBody, new MailAddress(email), null);
                }
                //LogEmails(null, null, null, subject, emailBody, email, null);
                return "2";
            }
            catch (Exception exception)
            {
                //Logger.Logger.LogException(exception.Message, exception);
                return exception.Message;
            }
        }

        public string GetTimeAMPM(string time)
        {
            TimeSpan ft = TimeSpan.Parse(time);
            DateTime ftTemp = DateTime.ParseExact(ft.ToString(), "HH:mm:ss", CultureInfo.InvariantCulture);
            string ftString = ftTemp.ToString("hh:mmtt");
            return ftString;
        }


    }
}