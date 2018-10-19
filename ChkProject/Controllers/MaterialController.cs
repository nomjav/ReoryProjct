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
        
             public ActionResult BuyMaterialIndex()
        {

            ItemBuyModel _ItemBuy = new ItemBuyModel();
            var material = _unitOfWork.ItemRepository.Get(x => x.IsDeleted == false);
            var buy_material = _unitOfWork.ItemBuyRepository.Get(x => x.IsDeleted == false);
            foreach (var item in material)
            {
                ItemModel _material = new ItemModel();
                _material.ItemId = item.ItemId;
                _material.ItemName = item.ItemName;
                _material.UnitPrice = item.UnitPrice;
                _material.QuantityPresent = item.QuantityPresent;
                _material.MeasureUnit = item.MeasureUnit;
                _material.CreatedBy = item.CreatedBy;
                _material.CreatedDate = item.CreatedDate;


                _ItemBuy.DDLItems.Add(_material);
            }
            foreach (var item in buy_material)
            {
                ItemBuyModel _b_material = new ItemBuyModel();
                _b_material.ItemId = item.ItemId;
                _b_material.ItemBuyId = item.ItemBuyId;
                _b_material.ItemName = material.Where(x => x.ItemId == item.ItemId).Select(y => y.ItemName).FirstOrDefault();
                _b_material.QuantityBuy = item.QuantityBuy;
                _b_material.BuyDate = item.BuyDate;
                _b_material.MeasureUnit = material.Where(x => x.ItemId == item.ItemId).Select(y => y.MeasureUnit).FirstOrDefault();
                _b_material.Description = item.Description;


                _ItemBuy.ListItemBuy.Add(_b_material);


            }
            return View("BuyMaterial", _ItemBuy);
        }
        public ActionResult Buy_Use_Material()
        {

            ItemUsedModel _ItemUsed = new ItemUsedModel();
            var material = _unitOfWork.ItemRepository.Get(x => x.IsDeleted == false);
            var used_material = _unitOfWork.ItemUsedRepository.Get(x => x.IsDeleted == false);
            var buy_material = _unitOfWork.ItemBuyRepository.Get(x => x.IsDeleted == false);
            foreach (var item in material)
            {
                ItemModel _material = new ItemModel();
                _material.ItemId = item.ItemId;
                _material.ItemName = item.ItemName;
                _material.UnitPrice = item.UnitPrice;
                _material.QuantityPresent = item.QuantityPresent;
                _material.MeasureUnit = item.MeasureUnit;
                _material.CreatedBy = item.CreatedBy;
                _material.CreatedDate = item.CreatedDate;


                _ItemUsed.DDLItems.Add(_material);
            }
            foreach (var item in used_material)
            {
                ItemUsedModel _u_material = new ItemUsedModel();
                _u_material.ItemId = item.ItemId;
                _u_material.ItemUsedId = item.ItemUsedId;
                _u_material.ItemName = material.Where(x => x.ItemId == item.ItemId).Select(y => y.ItemName).FirstOrDefault();
                _u_material.QuantityUsed = item.QuantityUsed;
                _u_material.UseDate = item.UseDate;
                _u_material.MeasureUnit = material.Where(x => x.ItemId == item.ItemId).Select(y => y.MeasureUnit).FirstOrDefault();
                _u_material.Description = item.Description;
              

                _ItemUsed.ListItemUsed.Add(_u_material);
               

            }
                return View("BuyUseMaterial", _ItemUsed);
        }


        [HttpPost]
        public ActionResult UseMaterial(ItemUsedModel UsedMaterial)
        {

            try
            {
                var user = _unitOfWork.UserRepository.GetSingle(t => t.UserName == User.Identity.Name);
                var item = _unitOfWork.ItemRepository.GetSingle(t => t.ItemId == UsedMaterial.ItemId);
                if (item.QuantityPresent > UsedMaterial.QuantityUsed)
                {

                    item.QuantityPresent = item.QuantityPresent - UsedMaterial.QuantityUsed.Value;
                    item.ModifiedDate = DateTime.Now;
                    if (user != null)
                    {
                        item.ModifiedBy = user.Id;
                    }
                    _unitOfWork.ItemRepository.Update(item);
                    _unitOfWork.Save();


                    ItemUsed _ItemUsed = new ItemUsed();
                    _ItemUsed.ItemId = UsedMaterial.ItemId;
                    _ItemUsed.QuantityUsed = UsedMaterial.QuantityUsed;
                    _ItemUsed.UseDate = UsedMaterial.UseDate;
                    _ItemUsed.CreatedDate = DateTime.Now;

                    if (user != null)
                    {
                        _ItemUsed.CreatedBy = user.Id;
                    }
                    _unitOfWork.ItemUsedRepository.Insert(_ItemUsed);
                    _unitOfWork.Save();
                    TempData["message"] = "success";
                }



            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Buy_Use_Material");
        }

        [HttpPost]
        public ActionResult BuyMaterial(ItemBuyModel BuyMaterial)
        {

            try
            {
                var user = _unitOfWork.UserRepository.GetSingle(t => t.UserName == User.Identity.Name);
                var item = _unitOfWork.ItemRepository.GetSingle(t => t.ItemId == BuyMaterial.ItemId);
                //if (item.QuantityPresent!=null)
                //{

                    item.QuantityPresent = item.QuantityPresent + BuyMaterial.QuantityBuy.Value;
                    item.ModifiedDate = DateTime.Now;
                    if (user != null)
                    {
                        item.ModifiedBy = user.Id;
                    }
                    _unitOfWork.ItemRepository.Update(item);
                    _unitOfWork.Save();


                    ItemBuy _ItemBuy = new ItemBuy();
                _ItemBuy.ItemId = BuyMaterial.ItemId;
                _ItemBuy.QuantityBuy = BuyMaterial.QuantityBuy;
                _ItemBuy.BuyDate = BuyMaterial.BuyDate;
                _ItemBuy.CreatedDate = DateTime.Now;

                    if (user != null)
                    {
                    _ItemBuy.CreatedBy = user.Id;
                    }
                    _unitOfWork.ItemBuyRepository.Insert(_ItemBuy);
                    _unitOfWork.Save();
                    TempData["message"] = "success";
              //  }



            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("BuyMaterialIndex");
        }

        [HttpPost]
        public JsonResult CheckDuplication(string ItemName)
        {
            var item = _unitOfWork.ItemRepository.GetSingle(t => t.ItemName == ItemName);
            return Json(item, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult UpdateBuy_UseMaterial(ItemUsedModel model)
        {
            try

            {
                var _itemused = _unitOfWork.ItemUsedRepository.GetSingle(t => t.ItemUsedId == model.ItemUsedId);
                var _user = _unitOfWork.UserRepository.GetSingle(t => t.UserName == User.Identity.Name);
                var item = _unitOfWork.ItemRepository.GetSingle(t => t.ItemId == model.ItemId);
                if (item.QuantityPresent + _itemused.QuantityUsed > model.QuantityUsed)
                {


                    if (_user != null)
                    {
                        _itemused.ModifiedBy = _user.Id;
                    }

                    if (model.IsDeleted == true)
                    {
                        _itemused.IsDeleted = true;
                        _itemused.DeletedBy = _user.Id;
                        _itemused.DeletedDate = DateTime.Now;

                    }
                    else
                    {
                        item.QuantityPresent = (item.QuantityPresent + _itemused.QuantityUsed.Value) - model.QuantityUsed.Value;
                        item.ModifiedDate = DateTime.Now;
                        if (_user != null)
                        {
                            item.ModifiedBy = _user.Id;
                        }
                        _unitOfWork.ItemRepository.Update(item);
                        _unitOfWork.Save();

                        _itemused.ItemId = model.ItemId;
                        _itemused.UseDate = model.UseDate;
                        _itemused.QuantityUsed = model.QuantityUsed;
                        // _item.Description = model.Description;
                        _itemused.ModifiedDate = DateTime.Now;
                    }


                    _unitOfWork.ItemUsedRepository.Update(_itemused);
                    _unitOfWork.Save();
                    TempData["message"] = "updated";


                }
                return Json(true, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult UpdateBuyMaterial(ItemBuyModel model)
        {
            try

            {
                var _itembuy = _unitOfWork.ItemBuyRepository.GetSingle(t => t.ItemBuyId == model.ItemBuyId);
                var _user = _unitOfWork.UserRepository.GetSingle(t => t.UserName == User.Identity.Name);
                var item = _unitOfWork.ItemRepository.GetSingle(t => t.ItemId == model.ItemId);
                if (item.QuantityPresent + _itembuy.QuantityBuy > model.QuantityBuy)
                {


                    if (_user != null)
                    {
                        _itembuy.ModifiedBy = _user.Id;
                    }

                    if (model.IsDeleted == true)
                    {
                        _itembuy.IsDeleted = true;
                        _itembuy.DeletedBy = _user.Id;
                        _itembuy.DeletedDate = DateTime.Now;

                    }
                    else
                    {
                        item.QuantityPresent = (item.QuantityPresent + _itembuy.QuantityBuy.Value) - model.QuantityBuy.Value;
                        item.ModifiedDate = DateTime.Now;
                        if (_user != null)
                        {
                            item.ModifiedBy = _user.Id;
                        }
                        _unitOfWork.ItemRepository.Update(item);
                        _unitOfWork.Save();

                        _itembuy.ItemId = model.ItemId;
                        _itembuy.BuyDate = model.BuyDate;
                        _itembuy.QuantityBuy = model.QuantityBuy;
                        // _item.Description = model.Description;
                        _itembuy.ModifiedDate = DateTime.Now;
                    }


                    _unitOfWork.ItemBuyRepository.Update(_itembuy);
                    _unitOfWork.Save();
                    TempData["message"] = "updated";


                }
                return Json(true, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
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