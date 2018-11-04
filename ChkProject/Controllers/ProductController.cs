
using Chakwal.Data.Data;
using Chakwal.Data.Repository;
using ChkProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chakwal.Data.MemberShip;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Drawing.Text;

namespace ChkProject.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        // GET: Product
        public ActionResult Index()
        {
            var products = _unitOfWork.ProductRepository.Get(x => x.IsDeleted == false);
            ProductModel pm = new ProductModel();
            foreach (var item in products)
            {
                ProductModel product = new ProductModel();
                product.ProductId = item.ProductId;
                product.ProductName = item.ProductName;
                product.UnitPrice = item.UnitPrice;
                product.CurrentQuantity = item.CurrentQuantity;
                product.Description = item.Description;
                product.CreatedBy = item.CreatedBy;
                product.CreatedDate = item.CreatedDate;
                product.BarCodeId = item.BarCodeId;
                product.BarCodeImage = item.BarCodeImage;
                pm.productList.Add(product);
            }
            return View(pm);
        }



        [HttpPost]
        public JsonResult CheckDuplication(string fieldvalue)
        {
            var pro = _unitOfWork.ProductRepository.GetSingle(t => t.ProductName == fieldvalue);
            return Json(pro, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RefreshProductGrid()
        {
            var products = _unitOfWork.ProductRepository.Get(x => x.IsDeleted == false);
            ProductModel pm = new ProductModel();
            foreach (var item in products)
            {
                ProductModel product = new ProductModel();
                product.ProductId = item.ProductId;
                product.ProductName = item.ProductName;
                product.UnitPrice = item.UnitPrice;
                product.CurrentQuantity = item.CurrentQuantity;
                product.Description = item.Description;
                product.CreatedBy = item.CreatedBy;
                product.CreatedDate = item.CreatedDate;
                product.BarCodeImage = item.BarCodeImage;

                pm.productList.Add(product);
            }
            return Json(pm, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateProduct(ProductModel model)
        {
            try
            {
                var pro = _unitOfWork.ProductRepository.GetSingle(t => t.ProductId == model.ProductId);
                var _user = _unitOfWork.UserRepository.GetSingle(t => t.UserName == User.Identity.Name);
                if (_user != null)
                {
                    pro.ModifiedBy = _user.Id;
                }

                if (model.IsDeleted == true)
                {
                    pro.IsDeleted = true;
                    pro.DeletedBy = _user.Id;
                    pro.DeletedDate = DateTime.Now;
                }
                else
                {
                    pro.ProductName = model.ProductName;
                    pro.UnitPrice = model.UnitPrice;
                    pro.CurrentQuantity = model.CurrentQuantity.Value;
                    pro.Description = model.Description;
                    pro.ModifiedDate = DateTime.Now;
                }

                TempData["message"] = "updated";
                _unitOfWork.ProductRepository.Update(pro);
                _unitOfWork.Save();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult AddProduct(ProductModel model)
        {
            try
            {
                Product p = new Product();
                p.ProductName = model.ProductName;
                p.UnitPrice = model.UnitPrice;
                p.CurrentQuantity = model.CurrentQuantity.Value;
                p.Description = model.Description;
                p.CreatedDate = DateTime.Now;
                model.BarCodeId = model.ProductName;
                p.BarCodeId = model.BarCodeId;
                var user = _unitOfWork.UserRepository.GetSingle(t => t.UserName == User.Identity.Name);
                if (user != null)
                {
                    p.CreatedBy = user.Id;
                }
                
                var bID = RemoveSpecialCharacters(model.BarCodeId);
                string mynewpath = Request.PhysicalApplicationPath + "Upload\\" + bID + ".jpg";
                GenerateBarCode(model.BarCodeId, mynewpath);
                p.BarCodeImage = "http://pehlwaninventory.net//Upload//" + bID + ".jpg";
                _unitOfWork.ProductRepository.Insert(p);
                _unitOfWork.Save();
                TempData["message"] = "success";
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index");
        }

        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }


        public void GenerateBarCode(string data, string path)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            Image img = b.Encode(BarcodeLib.TYPE.CODE39Extended, data, Color.Black, Color.White, 290, 120);
            img.Save(path, ImageFormat.Jpeg);
        }

    }


}