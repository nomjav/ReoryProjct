using System;
using System.Globalization;
using System.Threading;
using System.Web;
using Chakwal.Data.Data;
using Chakwal.Data.Repository;

namespace AcademyLockSmith.Data.MemberShip
{
    public class ChakwalContext
    {
        private readonly HttpContext context = HttpContext.Current;

        #region Ctor
        private ChakwalContext()
        {
            try
            {
                if (User == null)
                {
                    UnitOfWork unitOfWork = new UnitOfWork();

                    User user = unitOfWork.UserRepository.GetSingle(t => t.UserName == HttpContext.Current.User.Identity.Name && t.IsActive);
                    if (user != null && user.UserId > 0)
                    {
                        User = user;
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.Logger.LogException(ex.Message + ex.InnerException, ex);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 	Sets cookie
        /// </summary>
        /// <param name="application"> Application </param>
        /// <param name="key"> Key </param>
        /// <param name="val"> Value </param>
        private static void SetCookie(HttpApplication application, string key, string val)
        {
            HttpCookie cookie = new HttpCookie(key) { Value = val, Expires = string.IsNullOrEmpty(val) ? DateTime.UtcNow.AddMonths(-1) : DateTime.UtcNow.AddHours(128) };
            application.Response.Cookies.Remove(key);
            application.Response.Cookies.Add(cookie);
        }

        #endregion

        #region Properties

        /// <summary>
        /// 	Gets an instance of the Click2MeContext, which can be used to retrieve information about current context.
        /// </summary>
        public static ChakwalContext Current
        {
            get
            {
                if (HttpContext.Current == null)
                    return null;

                if (HttpContext.Current.Items["ALSContext"] == null)
                {
                    ChakwalContext context2 = new ChakwalContext();
                    if (!HttpContext.Current.Items.Contains("ALSContext"))
                    {
                        HttpContext.Current.Items.Add("ALSContext", context2);
                    }
                    return context2;
                }
                return (ChakwalContext)HttpContext.Current.Items["ALSContext"];
            }
        }

        /// <summary>
        /// 	Gets or sets an object item in the context by the specified key.
        /// </summary>
        /// <param name="key"> The key of the value to get. </param>
        /// <returns> The value associated with the specified key. </returns>
        public object this[string key]
        {
            get
            {
                if (context == null)
                {
                    return null;
                }

                if (context.Items[key] != null)
                {
                    return context.Items[key];
                }
                return null;
            }
            set
            {
                if (context != null)
                {
                    context.Items.Remove(key);
                    context.Items.Add(key, value);
                }
            }
        }

        public User User { get; set; }


        /// <summary>
        /// 	Sets the CultureInfo
        /// </summary>
        /// <param name="culture"> Culture </param>
        public void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }

        #endregion

    }
}
