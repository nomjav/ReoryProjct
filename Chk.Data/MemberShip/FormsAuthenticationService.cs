using Chakwal.Data.Data;
using Chakwal.Data.Repository;
using System;
using System.Web;
using System.Web.Security;


namespace AcademyLockSmith.Data.MemberShip
{
   public class FormsAuthenticationService
    {
       private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        
       private User _cachedUser;
       
        public string SignIn(User user)
        {
            var now = DateTime.UtcNow.ToLocalTime();

            var ticket = new FormsAuthenticationTicket(1, user.UserName, now, now.Add(TimeSpan.FromHours(24)), true, user.UserName, FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            _cachedUser = user;
            return encryptedTicket;
        }
        public virtual void SignOut()
        {
            _cachedUser = null;
            FormsAuthentication.SignOut();
        }

        public User GetAuthenticatedUser()
        {
            if (_cachedUser != null)
                return _cachedUser;

            if (HttpContext.Current == null ||
                !((HttpContext.Current.User).Identity).IsAuthenticated ||
                !(HttpContext.Current.User.Identity is FormsIdentity))
            {
                return null;
            }

            var formsIdentity = (FormsIdentity)HttpContext.Current.User.Identity;
            var user = GetAuthenticatedCustomerFromTicket(formsIdentity.Ticket);
            if (user != null && user.IsActive && !user.IsDeleted)
                _cachedUser = user;
            return _cachedUser;
        }

        public virtual User GetAuthenticatedCustomerFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var userName = ticket.UserData;

            if (String.IsNullOrWhiteSpace(userName))
                return null;
            var user = _unitOfWork.UserRepository.GetSingle(x => x.UserName == userName);
            return user;
        }

        public FormsAuthenticationTicket ExtractTicketFromCookie(HttpContext context, string cookielessTicket)
        {
            FormsAuthenticationTicket ticket = null;
            string encryptedTicket = null;
            try
            {
                //cookielessTicket = true;//CookielessHelperClass.UseCookieless(context, false, FormsAuthentication.CookieMode);
                if (true)
                {
                    encryptedTicket = cookielessTicket; //context.CookielessHelper.GetCookieValue('F');
                }

                if ((encryptedTicket != null) && (encryptedTicket.Length > 1))
                {
                    try
                    {
                        ticket = FormsAuthentication.Decrypt(encryptedTicket);
                    }
                    catch
                    {
                        throw new Exception("Failed to parse security token");
                    }

                    if (((ticket != null) && !ticket.Expired))
                    {
                        return ticket;
                    }
                    if ((ticket != null) && ticket.Expired)
                    {
                        ticket = null;
                    }
                }
            }
            catch
            {
                return null;
            }
            return ticket;
        }
    }
}
