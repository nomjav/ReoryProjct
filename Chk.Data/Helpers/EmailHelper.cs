using AcademyLockSmith.Data.MemberShip;

using AcademyLockSmith.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;
using System.Web;
using System.Globalization;
using System.Web.Hosting;
//using System.Data.Entity.Validation;
using Chakwal.Data.Repository;
using Chakwal.Data.Data;

namespace Chakwal.Data.Helpers.EmailHelper
{
    public class EmailHelper
    {


        public string GetTimeAMPM(string time)
        {
            TimeSpan ft = TimeSpan.Parse(time);
            DateTime ftTemp = DateTime.ParseExact(ft.ToString(), "HH:mm:ss", CultureInfo.InvariantCulture);
            string ftString = ftTemp.ToString("hh:mmtt");
            return ftString;
        }


    }
}