﻿
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
            var products = _unitOfWork.ProductRepository.Get();
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

            p.IsActive = model.IsActive;
            //p.IsActive = model.IsActive;
            _unitOfWork.ProductRepository.Insert(p);
            _unitOfWork.Save();

            var products = _unitOfWork.ProductRepository.Get();

            ProductModel pm = new ProductModel();
            foreach (var item in products)
            {
                ProductModel product = new ProductModel();
                product.ProductId = item.ProductId;
                product.ProductName = item.ProductName;
                product.UnitPrice = item.UnitPrice;
                product.CurrentQuantity = item.CurrentQuantity;
                product.Description = item.Description;
                product.CreatedDate = item.CreatedDate;
                product.CreatedBy = item.CreatedBy;
               
                pm.productList.Add(product);
            }


            return View("Index", pm);
        }
    }
}