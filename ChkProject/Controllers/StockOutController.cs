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
    public class StockOutController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        // GET: StockOut
        public ActionResult Index()
        {

            StockOutModel _StockOutModel = new StockOutModel();
            var productslist = _unitOfWork.ProductRepository.Get(x => x.IsDeleted == false);
            var companylocationlist = _unitOfWork.CompanyLocationRepository.Get(x => x.IsDeleted == false);
            var StockOutList = _unitOfWork.StockOutRepository.Get(x => x.IsDeleted == false);

            foreach (var item in productslist)
            {
                DDLProducts ddlproduct = new DDLProducts();
                ddlproduct.ProductId = item.ProductId;
                ddlproduct.ProductName = item.ProductName;
                _StockOutModel.DDLProduct.Add(ddlproduct);
            }
            foreach (var item in companylocationlist)
            {
                DDLCompanyLocation ddlcompanylocation = new DDLCompanyLocation();
                ddlcompanylocation.CompanyLocationId = item.LocationId;
                ddlcompanylocation.LocationName = item.LocationName;
                _StockOutModel.DDLCompanyLocation.Add(ddlcompanylocation);
            }
            foreach (var item in StockOutList)
            {
                StockOutModel tempStockOutProduct = new StockOutModel();
                tempStockOutProduct.StockOutId = item.StockOutId;
                tempStockOutProduct.ProductId = item.ProductId;
                tempStockOutProduct.DateOut = item.DateOut;
                tempStockOutProduct.StockOutLocation = item.StockOutLocation;
                tempStockOutProduct.LocationTo = item.LocationTo;
                tempStockOutProduct.Quantity = item.Quantity;
                tempStockOutProduct.Description = item.Description;
                tempStockOutProduct.ProductName = productslist.Where(x => x.ProductId == item.ProductId).Select(y => y.ProductName).FirstOrDefault();
                tempStockOutProduct.LocationName = companylocationlist.Where(x => x.LocationId == item.StockOutLocation).Select(y => y.LocationName).FirstOrDefault();
                _StockOutModel.StockOutList.Add(tempStockOutProduct);
            }
            return View(_StockOutModel);
        }






        [HttpPost]
        public ActionResult AddStockOut(StockOutModel model)

        {

            StockOut _StockOut = new StockOut();
            _StockOut.StockOutLocation = model.StockOutLocation;
            _StockOut.Quantity = model.Quantity;
            _StockOut.ProductId = model.ProductId;
            _StockOut.Description = model.Description;
            _StockOut.DateOut = model.DateOut;
            _StockOut.CreatedDate = DateTime.Now;
            var user = _unitOfWork.UserRepository.GetSingle(t => t.UserName == User.Identity.Name);
            if (user != null)
            {
                _StockOut.CreatedBy = user.Id;
            }
            _unitOfWork.StockOutRepository.Insert(_StockOut);
            _unitOfWork.Save();
            TempData["message"] = "success";
            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult UpdateStockOut(StockOutModel model)
        {
            try
            {
                var stockout = _unitOfWork.StockOutRepository.GetSingle(t => t.StockOutId == model.StockOutId);
                var _user = _unitOfWork.UserRepository.GetSingle(t => t.UserName == User.Identity.Name);
                if (_user != null)
                {
                    stockout.ModifiedBy = _user.Id;
                }

                if (model.IsDeleted == true)
                {
                    stockout.IsDeleted = true;
                    stockout.DeletedBy = _user.Id;
                    stockout.DeletedDate = DateTime.Now;

                }
                else
                {
                    stockout.ProductId = model.ProductId;
                    stockout.DateOut = model.DateOut;
                    stockout.Quantity = model.Quantity;
                    stockout.Description = model.Description;
                    stockout.ModifiedDate = DateTime.Now;
                }


                _unitOfWork.StockOutRepository.Update(stockout);
                _unitOfWork.Save();
                TempData["message"] = "updated";
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }









    }
}