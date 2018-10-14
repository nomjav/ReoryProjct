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
    public class CompanyController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        // GET: Company
        public ActionResult Index()
        {
            var _company = _unitOfWork.CompanyRepository.Get(x => x.IsDeleted == false);
            var _location = _unitOfWork.CompanyLocationRepository.Get(x => x.IsDeleted == false);
            CompanyModel Companies = new CompanyModel();
            foreach (var item in _company)
            {
                CompanyModel company = new CompanyModel();
                company.CompanyId = item.CompanyId;
                company.CompanyName = item.CompanyName;
                company.CompanyOwner = item.CompanyOwner;
                company.CompanyLogoPath = item.CompanyLogoPath;
                company.CompanyAddress = item.CompanyAddress;
                company.CreatedBy = item.CreatedBy;
                company.CreatedDate = item.CreatedDate;
                var loc = _location.Where(x => x.CompanyId == item.CompanyId).ToList();
                foreach (var _itemloc in loc)
                {
                    CompanyLocationModel companylocation = new CompanyLocationModel();
                    companylocation.LocationId = _itemloc.LocationId;
                    companylocation.CompanyId = _itemloc.CompanyId;
                    companylocation.LocationName = _itemloc.LocationName;
                    companylocation.LocationAddress = _itemloc.LocationAddress;
                    companylocation.MainLocation = _itemloc.MainLocation;
                    companylocation.CreatedBy = _itemloc.CreatedBy;
                    companylocation.CreatedDate = _itemloc.CreatedDate;
                    companylocation.CompanyName = item.CompanyName;
                    companylocation.CompanyOwner = item.CompanyOwner;
                    companylocation.CompanyAddress = item.CompanyAddress;
                    Companies.CompanylocationList.Add(companylocation);
                }
                    Companies.CompanyList.Add(company);
            }
            return View(Companies);
        }


        [HttpPost]
        public ActionResult AddCompany(CompanyModel model)

        {

            Company company = new Company();
            company.CompanyName = model.CompanyName;
            company.CompanyOwner = model.CompanyOwner;
            company.CompanyAddress = model.CompanyAddress;
            company.CreatedDate = DateTime.Now;
            company.IsActive = true;
            var user = _unitOfWork.UserRepository.GetSingle(t => t.UserName == User.Identity.Name);
            if (user != null)
            {
                company.CreatedBy = user.Id;
            }

            CompanyLocation _CompanyLocation = new CompanyLocation();
            _CompanyLocation.LocationName = model.LocationName;
            _CompanyLocation.LocationAddress = model.LocationAddress;
            _CompanyLocation.MainLocation = true;
            _CompanyLocation.CreatedDate = DateTime.Now;
            _CompanyLocation.IsActive = true;
            if (user != null)
            {
                _CompanyLocation.CreatedBy = user.Id;
            }
            _unitOfWork.CompanyLocationRepository.Insert(_CompanyLocation);
            _unitOfWork.CompanyRepository.Insert(company);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }



        [HttpPost]
        public JsonResult UpdateCompany(CompanyModel model)
        {
            try
            {
                var Company = _unitOfWork.CompanyRepository.GetSingle(t => t.CompanyId == model.CompanyId);
                var CompanyLocation = _unitOfWork.CompanyLocationRepository.GetSingle(t => t.CompanyId == model.CompanyId);
                var _user = _unitOfWork.UserRepository.GetSingle(t => t.UserName == User.Identity.Name);
                if (_user != null)
                {
                    Company.ModifiedBy = _user.Id;
                    CompanyLocation.ModifiedBy = _user.Id;
                }

                if (model.IsDeleted == true)
                {
                    Company.IsDeleted = true;
                    Company.DeletedBy = _user.Id;
                    Company.DeletedDate = DateTime.Now;
                    CompanyLocation.IsDeleted = true;
                    CompanyLocation.DeletedBy = _user.Id;
                    CompanyLocation.DeletedDate = DateTime.Now;

                }
                else
                {
                    Company.CompanyName = model.CompanyName;
                    Company.CompanyName = model.CompanyName;
                    Company.CompanyOwner = model.CompanyOwner;
                    Company.CompanyAddress = model.CompanyAddress;
                    Company.ModifiedDate = DateTime.Now;
                    CompanyLocation.LocationName = model.LocationAddress;
                    CompanyLocation.LocationName = model.LocationName;
                    CompanyLocation.ModifiedDate = DateTime.Now;
                }


                _unitOfWork.CompanyRepository.Update(Company);
                _unitOfWork.CompanyLocationRepository.Update(CompanyLocation);
                _unitOfWork.Save();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult CheckDuplication(string fieldvalue)
        {
            var Com = _unitOfWork.CompanyRepository.GetSingle(t => t.CompanyName == fieldvalue);
            return Json(Com, JsonRequestBehavior.AllowGet);
        }

    }
}