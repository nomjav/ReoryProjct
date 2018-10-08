﻿using Chakwal.Data.Data;
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
    public class StockInController : Controller   
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        // GET: StockIn
        public ActionResult Index()
        {

            StockInProductModel _StockInProductModel = new StockInProductModel();
            var productslist = _unitOfWork.ProductRepository.Get(x => x.IsDeleted == false);
            var companylocationlist = _unitOfWork.CompanyLocationRepository.Get(x => x.IsDeleted == false);
            var StockInProductList = _unitOfWork.StockInProductRepository.Get(x => x.IsDeleted == false);
            foreach (var item in productslist)
            {
                DDLProducts ddlproduct = new DDLProducts();
                ddlproduct.ProductId = item.ProductId;
                ddlproduct.ProductName = item.ProductName;
                _StockInProductModel.DDLProduct.Add(ddlproduct);
            }
            foreach (var item in companylocationlist)
            {
                DDLCompanyLocation ddlcompanylocation = new DDLCompanyLocation();
                ddlcompanylocation.CompanyLocationId = item.LocationId;
                ddlcompanylocation.LocationName = item.LocationName;
                _StockInProductModel.DDLCompanyLocation.Add(ddlcompanylocation);
            }

            foreach (var item in StockInProductList)
            {
                StockInProductModel tempStockInProduct = new StockInProductModel();
                tempStockInProduct.StockInId = item.StockInId;
                tempStockInProduct.ProductId = item.ProductId;
                tempStockInProduct.DateIn = item.DateIn;
                tempStockInProduct.StockInLocation = item.StockInLocation;
                tempStockInProduct.LocationFrom = item.LocationFrom;
                tempStockInProduct.Quantity = item.Quantity;
                tempStockInProduct.Description = item.Description;
                tempStockInProduct.ProductName = productslist.Where(x => x.ProductId == item.ProductId).Select(y=>y.ProductName).FirstOrDefault();
                tempStockInProduct.LocationName = companylocationlist.Where(x => x.LocationId == item.StockInLocation).Select(y => y.LocationName).FirstOrDefault();
                _StockInProductModel.StockInProductList.Add(tempStockInProduct);
            }
            return View(_StockInProductModel);
        }




        [HttpPost]
        public ActionResult AddStockIn(StockInProductModel model)

        {

            StockInProduct _StockInProduct = new StockInProduct();
            _StockInProduct.StockInLocation = model.StockInLocation;
            _StockInProduct.Quantity = model.Quantity;
            _StockInProduct.ProductId = model.ProductId;
            _StockInProduct.Description = model.Description;
            _StockInProduct.DateIn = model.DateIn;
            _StockInProduct.CreatedDate = DateTime.Now;
            var user = _unitOfWork.UserRepository.GetSingle(t => t.UserName == User.Identity.Name);
            if (user != null)
            {
                _StockInProduct.CreatedBy = user.Id;
            }
            _unitOfWork.StockInProductRepository.Insert(_StockInProduct);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult UpdateStockIn(StockInProductModel model)
        {
            try
            {
                var stockin = _unitOfWork.StockInProductRepository.GetSingle(t => t.StockInId == model.StockInId);
                var _user = _unitOfWork.UserRepository.GetSingle(t => t.UserName == User.Identity.Name);
                if (_user != null)
                {
                    stockin.ModifiedBy = _user.Id;
                }

                if (model.IsDeleted == true)
                {
                    stockin.IsDeleted = true;
                    stockin.DeletedBy = _user.Id;
                    stockin.DeletedDate = DateTime.Now;

                }
                else
                {
                    stockin.ProductId = model.ProductId;
                    stockin.DateIn = model.DateIn;
                    stockin.Quantity = model.Quantity;
                    stockin.Description = model.Description;
                    stockin.ModifiedDate = DateTime.Now;
                }


                _unitOfWork.StockInProductRepository.Update(stockin);
                _unitOfWork.Save();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }






    }
}