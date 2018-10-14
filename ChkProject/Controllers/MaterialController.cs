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
    public class MaterialController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        // GET: Item
        public ActionResult Index()
        {
            ItemModel _ItemModel = new ItemModel();
            var material = _unitOfWork.ItemRepository.Get(x => x.IsDeleted == false);
            foreach (var item in material)
            {
                ItemModel _material = new ItemModel();
                _material.ItemId = item.ItemId;
                _material.ItemName = item.ItemName;
                _material.UnitPrice = item.UnitPrice;
                _material.QuantityPresent = item.QuantityPresent;
                _material.MeasureUnit = item.MeasureUnit;
                // _material.Description = item.Description;
                _material.CreatedBy = item.CreatedBy;
                _material.CreatedDate = item.CreatedDate;


                _ItemModel.ItemList.Add(_material);
            }
            return View(_ItemModel);
        }


        [HttpPost]
        public JsonResult CheckDuplication(string ItemName)
        {
            var item = _unitOfWork.ItemRepository.GetSingle(t => t.ItemName == ItemName);
            return Json(item, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UpdateMaterial(ItemModel model)
        {
            try
            {
                var _item = _unitOfWork.ItemRepository.GetSingle(t => t.ItemId == model.ItemId);
                var _user = _unitOfWork.UserRepository.GetSingle(t => t.UserName == User.Identity.Name);
                if (_user != null)
                {
                    _item.ModifiedBy = _user.Id;
                }

                if (model.IsDeleted == true)
                {
                    _item.IsDeleted = true;
                    _item.DeletedBy = _user.Id;
                    _item.DeletedDate = DateTime.Now;

                }
                else
                {
                    _item.ItemName = model.ItemName;
                    _item.UnitPrice = model.UnitPrice;
                    _item.QuantityPresent = model.QuantityPresent;
                   // _item.Description = model.Description;
                    _item.ModifiedDate = DateTime.Now;
                }


                _unitOfWork.ItemRepository.Update(_item);
                _unitOfWork.Save();
                TempData["message"] = "updated";
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult AddMaterial(ItemModel model)

        {

            Item _Item = new Item();
            _Item.ItemName = model.ItemName;
            _Item.UnitPrice = model.UnitPrice;
            _Item.QuantityPresent = model.QuantityPresent;
           //_Item.Description = model.Description;
            _Item.CreatedDate = DateTime.Now;
            var user = _unitOfWork.UserRepository.GetSingle(t => t.UserName == User.Identity.Name);
            if (user != null)
            {
                _Item.CreatedBy = user.Id;
            }
            if (model.MeasureUnit == "1")
            {
                _Item.MeasureUnit = "Kg";
            }
            if (model.MeasureUnit == "2")
            {
                _Item.MeasureUnit = "Liter";
            }
            if (model.MeasureUnit == "3")
            {
                _Item.MeasureUnit = "Meter";
            }
            if (model.MeasureUnit == "4")
            {
                _Item.MeasureUnit = "Other";
            }
            else
            {
                _Item.MeasureUnit = "Other";
            }


            _unitOfWork.ItemRepository.Insert(_Item);
            _unitOfWork.Save();
            TempData["message"] = "success";
            return RedirectToAction("Index");
        }



    }
}