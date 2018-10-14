using Chakwal.Data.Repository;
using ChkProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChkProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            HomeModel hm = new HomeModel();
            var totalStocks = _unitOfWork.StockOutRepository.GetAsQuerable();
            hm.totalSales = totalStocks.Where(x=>x.IsDeleted == false).Count();
            hm.monthlyIncome = totalStocks.Where(x => x.DateOut.Month == DateTime.Now.Month && x.IsDeleted == false).Sum(x => x.Product.UnitPrice * x.Quantity);
            hm.totalIncome = totalStocks.Where(x => x.IsDeleted == false).Sum(x => x.Product.UnitPrice * x.Quantity);
            var day = DateTime.Now.AddDays(-7);
            ViewBag.chart1 = totalStocks.Where(x => x.IsDeleted == false && x.DateOut >= day && x.DateOut <= DateTime.Now).Select(x=>x.Quantity).ToList();
            ViewBag.chart1Label = totalStocks.Where(x => x.IsDeleted == false && x.DateOut >= day && x.DateOut <= DateTime.Now).Select(x => x.DateOut.ToString()).ToList();

            hm.Materials = _unitOfWork.ItemRepository.Get(x=>x.IsDeleted == false && x.CreatedDate >= day && x.CreatedDate <= DateTime.Now);

            return View(hm);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}