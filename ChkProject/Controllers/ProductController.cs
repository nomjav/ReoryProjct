using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChkProject.Models;

namespace ChkProject.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            ProductModel _ProductModel = new ProductModel();
            return View(_ProductModel);
        }
    }
}