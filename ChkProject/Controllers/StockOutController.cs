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
           // var user = _unitOfWork.UserRepository.GetSingle(t => t.UserName == User.Identity.Name);
            var lcl_user = _unitOfWork.LocalUserRepository.GetSingle(t => t.UserName == User.Identity.Name);

            var productslist = _unitOfWork.ProductRepository.Get(x => x.IsDeleted == false);
            var companylocationlist = _unitOfWork.CompanyLocationRepository.Get(x => x.IsDeleted == false);
            var StockOutList = _unitOfWork.StockOutRepository.Get(x => x.IsDeleted == false);

            foreach (var item in productslist)
            {
                DDLProducts ddlproduct = new DDLProducts();
                ddlproduct.ProductId = item.ProductId;
                ddlproduct.ProductName = item.ProductName;
                ddlproduct.UnitPrice = item.UnitPrice;
                ddlproduct.CurrentQuantity = item.CurrentQuantity;

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
                tempStockOutProduct.SoldUnitPrice = item.SoldUnitPrice;
                tempStockOutProduct.Quantity = item.Quantity;
                tempStockOutProduct.Description = item.Description;
                tempStockOutProduct.ProductName = productslist.Where(x => x.ProductId == item.ProductId).Select(y => y.ProductName).FirstOrDefault();
                tempStockOutProduct.LocationName = companylocationlist.Where(x => x.LocationId == item.StockOutLocation).Select(y => y.LocationName).FirstOrDefault();
              
                _StockOutModel.StockOutList.Add(tempStockOutProduct);
            }
            _StockOutModel.selectedLocationid = lcl_user.LocationId;
            return View(_StockOutModel);
        }

        public String GenerateUniqueId()
        {

            long billnumber = 1000000;
            var curr_billnum = _unitOfWork.BillRepository.Get().LastOrDefault();
            if (curr_billnum != null)
            {
                billnumber = Convert.ToInt64(curr_billnum.Billnumber) + 1;
            }

            return billnumber.ToString();
        }


        public ActionResult GenerateBill()
        {
            BillModel _BillModel = new BillModel();
            StockOutModel _StockOutModel = new StockOutModel();
            // var user = _unitOfWork.UserRepository.GetSingle(t => t.UserName == User.Identity.Name);
            var lcl_user = _unitOfWork.LocalUserRepository.GetSingle(t => t.UserName == User.Identity.Name);

            var productslist = _unitOfWork.ProductRepository.Get(x => x.IsDeleted == false);
            var companylocationlist = _unitOfWork.CompanyLocationRepository.Get(x => x.IsDeleted == false);
            var StockOutList = _unitOfWork.StockOutRepository.Get(x => x.IsDeleted == false);

            foreach (var item in productslist)
            {
                DDLProducts ddlproduct = new DDLProducts();
                ddlproduct.ProductId = item.ProductId;
                ddlproduct.ProductName = item.ProductName;
                ddlproduct.UnitPrice = item.UnitPrice;
                ddlproduct.CurrentQuantity = item.CurrentQuantity;

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
                tempStockOutProduct.SoldUnitPrice = item.SoldUnitPrice;
                tempStockOutProduct.Quantity = item.Quantity;
                tempStockOutProduct.Description = item.Description;
                tempStockOutProduct.ProductName = productslist.Where(x => x.ProductId == item.ProductId).Select(y => y.ProductName).FirstOrDefault();
                tempStockOutProduct.LocationName = companylocationlist.Where(x => x.LocationId == item.StockOutLocation).Select(y => y.LocationName).FirstOrDefault();

                _StockOutModel.StockOutList.Add(tempStockOutProduct);
            }
            _StockOutModel.selectedLocationid = lcl_user.LocationId;

            _BillModel.Billnumber= GenerateUniqueId();
            _BillModel.StockoutDetail = _StockOutModel;
            return View("GenerateBill", _BillModel);
        }



        [HttpPost]
        public JsonResult SaveBill(List<BillModel> BillItems)
        {
            var bill_item = BillItems.Where(x => x.BillType == "BillItem").ToList();
            var credit_debitlist = BillItems.Where(x => x.BillType == "Credit_Debit").ToList();

            foreach (var item in bill_item)
            {
                Bill _Bill = new Bill();
                _Bill.Description = item.Description;
                _Bill.Quantity = item.Quantity;
                _Bill.Price = item.Price;
                _Bill.CreatedDate = DateTime.Now;
                _Bill.BillDate = DateTime.Now;
                _Bill.Billnumber = item.Billnumber;
                _Bill.BuyerName = item.BuyerName;
                var user = _unitOfWork.UserRepository.GetSingle(t => t.UserName == User.Identity.Name);
                var localUSer = _unitOfWork.LocalUserRepository.GetSingle(lc => lc.UserName == User.Identity.Name);
                if (user != null)
                {
                    _Bill.CreatedBy = user.Id;
                }
                _unitOfWork.BillRepository.Insert(_Bill);
                _unitOfWork.Save();
                TempData["message"] = "success";

            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddStockOutWithBarcode(List<string> Info)
        {
            try
            {
                var q = from x in Info
                        group x by x into g
                        let count = g.Count()
                        orderby count descending
                        select new { Value = g.Key, Count = count };
                foreach (var x in q)
                {
                    //Console.WriteLine("Value: " + x.Value + " Count: " + x.Count);

                    StockOut _StockOutProduct = new StockOut();
                    var product = _unitOfWork.ProductRepository.GetSingle(p => p.ProductName == x.Value);
                    if (product != null)
                    {

                        _StockOutProduct.Quantity = x.Count;
                        _StockOutProduct.ProductId = product.ProductId;
                        _StockOutProduct.Description = product.Description;
                        _StockOutProduct.DateOut = DateTime.Now;
                        _StockOutProduct.CreatedDate = DateTime.Now;
                        _StockOutProduct.SoldUnitPrice = product.UnitPrice;
                        var user = _unitOfWork.UserRepository.GetSingle(t => t.UserName == User.Identity.Name);
                        var localUSer = _unitOfWork.LocalUserRepository.GetSingle(lc => lc.UserName == User.Identity.Name);
                        _StockOutProduct.StockOutLocation = 1;
                        if (user != null)
                        {
                            _StockOutProduct.CreatedBy = user.Id;
                        }
                        _unitOfWork.StockOutRepository.Insert(_StockOutProduct);
                        _unitOfWork.Save();
                        TempData["message"] = "success";

                    }

                }
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                TempData["message"] = "error";
                return Json(false, JsonRequestBehavior.AllowGet);
            }


        }



        [HttpPost]
        public ActionResult AddStockOut(StockOutModel model)

        {

            StockOut _StockOut = new StockOut();
            _StockOut.StockOutLocation = model.StockOutLocation;
            _StockOut.Quantity = model.Quantity;
            _StockOut.SoldUnitPrice = model.SoldUnitPrice;
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
            var pro = _unitOfWork.ProductRepository.GetSingle(t => t.ProductId == model.ProductId);
            if (pro != null)
            {
                if (pro.CurrentQuantity > model.Quantity)
                {
                    pro.CurrentQuantity = pro.CurrentQuantity - model.Quantity;

                    _unitOfWork.ProductRepository.Update(pro);
                }
              

            }


            _unitOfWork.Save();
            TempData["message"] = "success";
            return RedirectToAction("Index");
        }



        [HttpPost]
        public ActionResult AddStockOutList(List<StockOutModel> model)

        {
            foreach (var stk in model)
            {
                StockOut _StockOut = new StockOut();
                _StockOut.StockOutLocation = stk.StockOutLocation;
                _StockOut.Quantity = stk.Quantity;
                _StockOut.ProductId = stk.ProductId;
                _StockOut.Description = stk.Description;
                _StockOut.DateOut = stk.DateOut;
                _StockOut.CreatedDate = DateTime.Now;
                var user = _unitOfWork.UserRepository.GetSingle(t => t.UserName == User.Identity.Name);
                if (user != null)
                {
                    _StockOut.CreatedBy = user.Id;
                }
                _unitOfWork.StockOutRepository.Insert(_StockOut);
                var pro = _unitOfWork.ProductRepository.GetSingle(t => t.ProductId == stk.ProductId);
                if (pro != null)
                {
                    if (pro.CurrentQuantity > stk.Quantity)
                    {
                        pro.CurrentQuantity = pro.CurrentQuantity - stk.Quantity;

                        _unitOfWork.ProductRepository.Update(pro);
                    }


                }
            }

          
          
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
                    stockout.SoldUnitPrice = model.SoldUnitPrice;
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