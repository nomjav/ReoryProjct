
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
    public class ProductController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        // GET: Product
        public ActionResult Index()
       {
            var products = _unitOfWork.ProductRepository.context.Products.Where(x => x.IsDeleted == false).ToList();
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
        public JsonResult UpdateProduct(ProductModel model)
        {
            try {
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
                    pro.CurrentQuantity = model.CurrentQuantity;
                    pro.Description = model.Description;
                    pro.ModifiedDate = DateTime.Now;
                }
              
                
                _unitOfWork.ProductRepository.Update(pro);
                _unitOfWork.Save();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
           
        }

        [HttpPost]
        public ActionResult AddProduct(ProductModel model)

        {

                Product p = new Product();
                p.ProductName = model.ProductName;
                p.UnitPrice = model.UnitPrice;
                p.CurrentQuantity = model.CurrentQuantity;
                p.Description = model.Description;
                p.CreatedDate = DateTime.Now;
                var user = _unitOfWork.UserRepository.GetSingle(t => t.UserName == User.Identity.Name);
                if (user != null)
                {
                    p.CreatedBy = user.Id;
                }
                _unitOfWork.ProductRepository.Insert(p);
                _unitOfWork.Save();
          return   RedirectToAction("Index");
        }
    }
}