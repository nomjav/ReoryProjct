using Chakwal.Data.Data;
using Chakwal.Data.Repository;
using ChkProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chakwal.Data.MemberShip;

namespace ChkProject.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        // GET: User
        public ActionResult Index()
        {
            UserModel _userModel = new UserModel();
            var material = _unitOfWork.LocalUserRepository.Get(x => x.IsDeleted == false);
            foreach (var item in material)
            {
                UserModel _user = new UserModel();
                _user.UserId = item.UserId;
                _user.FirstName = item.FirstName;
                _user.LastName = item.LastName;
                _user.RoleId = item.RoleId;
                _user.CompanyId = item.CompanyId;
                _user.UserName = item.UserName;
                _user.Email = item.Email;
                // _material.Description = item.Description;
                _user.CreatedBy = item.CreatedBy;
                _user.CreatedDate = item.CreatedDate;


                _userModel.UserList.Add(_user);
            }
            return View(_userModel);
        }
    }
}