using System;

using System.Web.Security;
using AcademyLockSmith.Data.Helpers;
using System.Collections.Generic;
using System.Linq;
using Chakwal.Data.Repository;
using Chakwal.Data.Data;
using AcademyLockSmith.Data.MemberShip;

namespace Chakwal.Data.MemberShip
{
    //public class MemberShip : MembershipProvider
    //{

    //    /// <summary>
    //    /// 	Indicates whether the membership provider is configured to allow users to reset their passwords.
    //    /// </summary>
    //    /// <value> </value>
    //    /// <returns> true if the membership provider supports password reset; otherwise, false. The default is true. </returns>
    //    public override bool EnablePasswordReset
    //    {
    //        get { return true; }
    //    }

    //    /// <summary>
    //    /// 	Indicates whether the membership provider is configured to allow users to retrieve their passwords.
    //    /// </summary>
    //    /// <value> </value>
    //    /// <returns> true if the membership provider is configured to support password retrieval; otherwise, false. The default is false. </returns>
    //    public override bool EnablePasswordRetrieval
    //    {
    //        get { return true; }
    //    }

    //    /// <summary>
    //    /// 	Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.
    //    /// </summary>
    //    /// <value> </value>
    //    /// <returns> The number of invalid password or password-answer attempts allowed before the membership user is locked out. </returns>
    //    public override int MaxInvalidPasswordAttempts
    //    {
    //        get { return 5; }
    //    }

    //    /// <summary>
    //    /// 	Gets the minimum number of special characters that must be present in a valid password.
    //    /// </summary>
    //    /// <value> </value>
    //    /// <returns> The minimum number of special characters that must be present in a valid password. </returns>
    //    public override int MinRequiredNonAlphanumericCharacters
    //    {
    //        get { return 0; }
    //    }

    //    /// <summary>
    //    /// 	Gets the minimum length required for a password.
    //    /// </summary>
    //    /// <value> </value>
    //    /// <returns> The minimum length required for a password. </returns>
    //    public override int MinRequiredPasswordLength
    //    {
    //        get { return 3; }
    //    }

    //    /// <summary>
    //    /// 	Gets the number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
    //    /// </summary>
    //    /// <value> </value>
    //    /// <returns> The number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out. </returns>
    //    public override int PasswordAttemptWindow
    //    {
    //        get { return 1; }
    //    }

    //    /// <summary>
    //    /// 	Gets a value indicating whether the membership provider is configured to require the user to answer a password question for password reset and retrieval.
    //    /// </summary>
    //    /// <value> </value>
    //    /// <returns> true if a password answer is required for password reset and retrieval; otherwise, false. The default is true. </returns>
    //    public override bool RequiresQuestionAndAnswer
    //    {
    //        get { return false; }
    //    }

    //    /// <summary>
    //    /// 	The name of the application using the custom membership provider.
    //    /// </summary>
    //    /// <value> </value>
    //    /// <returns> The name of the application using the custom membership provider. </returns>
    //    public override string ApplicationName
    //    {
    //        get { return "Click2Me"; }
    //        set { }
    //    }

    //    /// <summary>
    //    /// 	Gets a value indicating whether the membership provider is configured to require a unique e-mail address for each user name.
    //    /// </summary>
    //    /// <value> </value>
    //    /// <returns> true if the membership provider requires a unique e-mail address; otherwise, false. The default is true. </returns>
    //    public override bool RequiresUniqueEmail
    //    {
    //        get { return true; }
    //    }

    //    #region "Unsupported methods"

    //    public override MembershipPasswordFormat PasswordFormat
    //    {
    //        get { return MembershipPasswordFormat.Encrypted; }
    //    }

    //    public override string PasswordStrengthRegularExpression
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
    //                                                         string newPasswordAnswer)
    //    {
    //        throw new NotSupportedException();
    //    }


    //    public override string ResetPassword(string username, string answer)
    //    {
    //        throw new NotSupportedException();
    //    }

    //    public override bool UnlockUser(string userName)
    //    {
    //        throw new NotSupportedException();
    //    }


    //    public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion,
    //                                              string passwordAnswer, bool isApproved, object providerUserKey,
    //                                              out MembershipCreateStatus status)
    //    {
    //        throw new NotSupportedException();
    //    }

    //    public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize,
    //                                                              out int totalRecords)
    //    {
    //        throw new NotSupportedException();
    //    }

    //    public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize,
    //                                                             out int totalRecords)
    //    {
    //        throw new NotSupportedException();
    //    }

    //    public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
    //    {
    //        throw new NotSupportedException();
    //    }

    //    public override int GetNumberOfUsersOnline()
    //    {
    //        throw new NotSupportedException();
    //    }

    //    #endregion

    //    /// <summary>
    //    /// 	Verifies that the specified user name and password exist in the data source.
    //    /// </summary>
    //    /// <param name="username"> The name of the user to validate. </param>
    //    /// <param name="password"> The password for the specified user. </param>
    //    /// <returns> true if the specified username and password are valid; otherwise, false. </returns>
    //    public override bool ValidateUser(string username, string password)
    //    {

    //        //var user = unitOfWork.UserRepository.GetSingle(x => x.Username.Equals(username)
    //        //    && (x.Password.Length > 18 ? (SecurityHelper.Decrypt(x.Password).Equals(password)) : x.Password.Equals(password)) && x.Active && x.Role.Active);

    //        //var users = unitOfWork.UserRepository.Get(x => x.Username.Equals(username) && x.Active && x.Role.Active);
    //        UnitOfWork unitOfWork = new UnitOfWork();
    //        var context = unitOfWork.UserRepository.context;
    //        var users = context.Users.Where(x => x.UserName.Equals(username) && x.IsActive && x.Role.IsActive);

    //        if (users.Count() > 0)
    //        {
    //            foreach (var user in users)
    //            {
    //                if (SecurityHelper.IsEncrypted(user.Password))
    //                {
    //                    var pass = SecurityHelper.Decrypt(user.Password);
    //                    if (pass.Equals(password))
    //                    {
    //                        ChakwalContext.Current.User = user;
    //                        return true;
    //                    }
    //                }
    //                else
    //                {
    //                    if (user.Password.Equals(password))
    //                    {
    //                        ChakwalContext.Current.User = user;
    //                        return true;
    //                    }
    //                }
    //            }
    //        }
    //        //Logger.Error("Login Failed" + "User Name: " + username + "  Password: " + password);
    //        return false;

    //        //if (user != null && user.Id > 0)
    //        //{
    //        //    ALSContext.Current.User = user;

    //        //    return true;
    //        //}
    //        ////Logger.Error("Login Failed" + "User Name: " + username + "  Password: " + password);
    //        //return false;
    //    }

    //    /// <summary>
    //    /// 	Processes a request to update the password for a membership user.
    //    /// </summary>
    //    /// <param name="username"> The user to update the password for. </param>
    //    /// <param name="oldPassword"> The current password for the specified user. </param>
    //    /// <param name="newPassword"> The new password for the specified user. </param>
    //    /// <returns> true if the password was updated successfully; otherwise, false. </returns>
    //    public override bool ChangePassword(string username, string oldPassword, string newPassword)
    //    {
    //        //UnitOfWork unitOfWork = new UnitOfWork();
    //        //var user = unitOfWork.UserRepository.GetSingle(x => x.Username.Equals(username) && x.Password.Equals(oldPassword) && x.Active);

    //        //if (user != null && user.Password == oldPassword)
    //        //{
    //        //    user.Password = newPassword;
    //        //    user.PasswordChangedOn = DateTime.Now;
    //        //    unitOfWork.UserRepository.Update(user);
    //        //    unitOfWork.Save();
    //        //    return true;
    //        //}

    //        // ChangePassword will throw an exception rather than return false in certain failure scenarios.
    //        bool changePasswordSucceeded = false;
    //        try
    //        {
    //            using (var context = new UnitOfWork())
    //            {
    //                var usrId = ChakwalContext.Current != null ? ChakwalContext.Current.User.UserId : 0;
    //                if (usrId != 0)
    //                {
    //                    var user = context.UserRepository.GetSingle(x => x.UserId == usrId);
    //                    if (user != null)
    //                    {
    //                        if (SecurityHelper.IsEncrypted(user.Password))
    //                        {
    //                            var pass = SecurityHelper.Decrypt(user.Password);
    //                            if (pass.Equals(oldPassword))
    //                            {
    //                                user.Password = SecurityHelper.Encrypt(newPassword);
    //                                user.ModifiedDate = DateTime.Now;
    //                                context.UserRepository.Update(user);
    //                                context.Save();
    //                                changePasswordSucceeded = true;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            if (user.Password.Equals(oldPassword))
    //                            {
    //                                user.Password = SecurityHelper.Encrypt(newPassword);
    //                                user.ModifiedDate = DateTime.Now;
    //                                context.UserRepository.Update(user);
    //                                context.Save();
    //                                changePasswordSucceeded = true;
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        changePasswordSucceeded = false;
    //                    }
    //                }
    //                else
    //                {
    //                    var users = context.UserRepository.Get(t => t.UserName.Equals(username));
    //                    if (users.Count > 0)
    //                    {
    //                        foreach (var user in users)
    //                        {
    //                            if (SecurityHelper.IsEncrypted(user.Password))
    //                            {
    //                                var pass = SecurityHelper.Decrypt(user.Password);
    //                                if (pass.Equals(oldPassword))
    //                                {
    //                                    user.Password = SecurityHelper.Encrypt(newPassword);
    //                                    user.ModifiedDate = DateTime.Now;
    //                                    context.UserRepository.Update(user);
    //                                    context.Save();
    //                                    changePasswordSucceeded = true;
    //                                    break;
    //                                }
    //                            }
    //                            else
    //                            {
    //                                if (user.Password.Equals(oldPassword))
    //                                {
    //                                    user.Password = SecurityHelper.Encrypt(newPassword);
    //                                    user.ModifiedDate = DateTime.Now;
    //                                    context.UserRepository.Update(user);
    //                                    context.Save();
    //                                    changePasswordSucceeded = true;
    //                                    break;
    //                                }
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        changePasswordSucceeded = false;
    //                    }
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //Logger.Logger.LogException(ex.Message, ex);
    //            changePasswordSucceeded = false;
    //        }

    //        return changePasswordSucceeded;
    //    }

    //    /// <summary>
    //    /// 	Removes a user from the membership data source.
    //    /// </summary>
    //    /// <param name="username"> The name of the user to delete. </param>
    //    /// <param name="deleteAllRelatedData"> true to delete data related to the user from the database; false to leave data related to the user in the database. </param>
    //    /// <returns> true if the user was successfully deleted; otherwise, false. </returns>
    //    public override bool DeleteUser(string username, bool deleteAllRelatedData)
    //    {
    //        UnitOfWork unitOfWork = new UnitOfWork();
    //        User us = unitOfWork.UserRepository.GetSingle(t => t.UserName == username);
    //        if (us != null)
    //        {
    //            unitOfWork.UserRepository.Delete(us);
    //            unitOfWork.Save();
    //            return true;
    //        }
    //        return false;
    //    }

    //    /// <summary>
    //    /// 	Gets the password for the specified user name from the data source.
    //    /// </summary>
    //    /// <param name="username"> The user to retrieve the password for. </param>
    //    /// <param name="answer"> The password answer for the user. </param>
    //    /// <returns> The password for the specified user name. </returns>
    //    public override string GetPassword(string username, string answer)
    //    {
    //        //User user = new User(User.Columns.EmailAddress, username);
    //        //if (user.UserID > 0)
    //        //{
    //        //    return user.Password;
    //        //}
    //        //else 
    //        return null;
    //    }

    //    /// <summary>
    //    /// 	Updates information about a user in the data source.
    //    /// </summary>
    //    /// <param name="mUser"> A <see cref="T:System.Web.Security.MembershipUser" /> object that represents the user to update and the updated information for the user. </param>
    //    public override void UpdateUser(MembershipUser mUser)
    //    {
    //        UnitOfWork unitOfWork = new UnitOfWork();
    //        User user = unitOfWork.UserRepository.GetSingle(t => t.Email == mUser.Email);
    //        if (user != null)
    //            if (user.UserId > 0)
    //            {
    //                user.ModifiedDate = mUser.CreationDate;
    //                unitOfWork.UserRepository.Update(user);
    //                unitOfWork.Save();
    //            }
    //    }

    //    /// <summary>
    //    /// 	Gets information from the data source for a user. Provides an option to update the last-activity date/time stamp for the user.
    //    /// </summary>
    //    /// <param name="username"> The name of the user to get information for. </param>
    //    /// <param name="userIsOnline"> true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user. </param>
    //    /// <returns> A <see cref="T:System.Web.Security.MembershipUser" /> object populated with the specified user's information from the data source. </returns>
    //    public override MembershipUser GetUser(string username, bool userIsOnline)
    //    {
    //        UnitOfWork unitOfWork = new UnitOfWork();
    //        User user = unitOfWork.UserRepository.GetSingle(t => t.UserName == username);

    //        if (user != null && user.UserId > 0)
    //        {
    //            return new MembershipUser("", user.Email, user.Email, user.Email, null, null, true, false,
    //                                      user.CreatedDate, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow,
    //                                      DateTime.MaxValue);
    //        }
    //        return null;
    //    }

    //    /// <summary>
    //    /// 	Gets user information from the data source based on the unique identifier for the membership user. Provides an option to update the last-activity date/time stamp for the user.
    //    /// </summary>
    //    /// <param name="providerUserKey"> The unique identifier for the membership user to get information for. </param>
    //    /// <param name="userIsOnline"> true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user. </param>
    //    /// <returns> A <see cref="T:System.Web.Security.MembershipUser" /> object populated with the specified user's information from the data source. </returns>
    //    public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
    //    {
    //        return GetUser(providerUserKey.ToString(), userIsOnline);
    //    }

    //    /// <summary>
    //    /// 	Gets the user name associated with the specified e-mail address.
    //    /// </summary>
    //    /// <param name="email"> The e-mail address to search for. </param>
    //    /// <returns> The user name associated with the specified e-mail address. If no match is found, return null. </returns>
    //    public override string GetUserNameByEmail(string email)
    //    {
    //        var membershipUser = GetUser(email, false);
    //        return membershipUser != null ? membershipUser.Email : "";
    //    }
    //    // To check that the specified user is active or not
    //    public bool IsUserActive(string userName)
    //    {
    //        UnitOfWork unitOfWork = new UnitOfWork();
    //        var isActive = false;
    //        var user = unitOfWork.UserRepository.GetSingle(t => t.UserName == userName);
    //        if (user != null)
    //        {
    //            isActive = user.IsActive;
    //        }
    //        return isActive;
    //    }

    //    // to block the user like if wrong user name or password is entered more than 3 times
    //    public void BlockUser(string userName)
    //    {
    //        UnitOfWork unitOfWork = new UnitOfWork();
    //        var user = unitOfWork.UserRepository.GetSingle(t => t.UserName == userName);
    //        if (user != null)
    //        {
    //            user.IsActive = false;
    //            unitOfWork.UserRepository.Update(user);
    //            unitOfWork.Save();
    //        }
    //    }

    //}
}
