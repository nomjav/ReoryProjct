using Chakwal.Data.Repository;
using ChkProject.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            try
            {
                var totalStocks = _unitOfWork.StockOutRepository.GetAsQuerable();
                hm.totalSales = totalStocks.Where(x => x.IsDeleted == false).Count();
                if (totalStocks.Count() != 0)
                {
                    hm.monthlyIncome = totalStocks.Where(x => x.DateOut.Month == DateTime.Now.Month && x.IsDeleted == false).Sum(x=>x.Quantity * x.SoldUnitPrice);
                    //if (monthlyIncome.Count != 0)
                    //{
                    //    var quantity = monthlyIncome.Sum(x => x.Quantity);
                    //    var price = monthlyIncome.Sum(x => x.SoldUnitPrice);
                    //    hm.monthlyIncome = quantity * price;
                    //}
                }

                var totaStockInYEAR = _unitOfWork.StockInProductRepository.Get(x => x.CreatedDate.Year == DateTime.Now.Year && x.IsDeleted == false).Select(x => new { x.Quantity, x.DateIn });
                ViewBag.StockIn = totaStockInYEAR.Select(x=>x.Quantity);
                var totaStockOutYEAR = _unitOfWork.StockOutRepository.Get(x => x.CreatedDate.Year == DateTime.Now.Year && x.IsDeleted == false).Select(x => x.Quantity);
                ViewBag.StockOut = totaStockOutYEAR;
                ViewBag.Months = totaStockInYEAR.Select(x => x.DateIn.ToString("MMMM")).Distinct();

                var weeklyDta = _unitOfWork.StockInProductRepository.Get(x => x.IsDeleted == false && x.DateIn.Month == DateTime.Now.Month).Take(10);

                var result =
                        from s in weeklyDta.Take(7)
                        group s by new { date = new DateTime(s.DateIn.Year, s.DateIn.Month, 1) } into g
                        select new
                        {
                            read_date = g.Key.date,
                            T1 = g.Sum(x => x.Quantity)
                            
                        };
                ViewBag.weeklyData = result.Select(x => x.T1);
                ViewBag.weeklyDates = result.Select(x => x.read_date.ToShortDateString());

                if (hm.monthlyIncome == null)
                {
                    hm.monthlyIncome = 0;
                }
                hm.totalIncome = totalStocks.Where(x => x.IsDeleted == false).Sum(x => x.SoldUnitPrice * x.Quantity);
                if(hm.totalIncome == null)
                {
                    hm.totalIncome = 0;
                }
                var day = DateTime.Now.AddDays(-7);
                ViewBag.chart1 = totalStocks.Where(x => x.IsDeleted == false && x.DateOut >= day && x.DateOut <= DateTime.Now).Select(x => x.Quantity).ToList();
                ViewBag.chart1Label = totalStocks.Where(x => x.IsDeleted == false && x.DateOut >= day && x.DateOut <= DateTime.Now).Select(x => x.DateOut.ToString()).ToList();
                var itemBuy = _unitOfWork.ItemBuyRepository.Get(x => x.IsDeleted == false && x.CreatedDate >= day && x.CreatedDate <= DateTime.Now);
                hm.Materials = new List<ItemBuyModel>();
                foreach (var item in itemBuy)
                {
                    var items = _unitOfWork.ItemRepository.GetSingle(x => x.ItemId == item.ItemId);
                    ItemBuyModel ibm = new ItemBuyModel();
                    ibm.ItemName = items.ItemName;
                    ibm.MeasureUnit = items.UnitPrice;
                    ibm.CreatedDate = item.CreatedDate;
                    ibm.QuantityBuy = item.QuantityBuy;
                    hm.Materials.Add(ibm);
                }
                var itemUsed = _unitOfWork.ItemUsedRepository.Get(x => x.IsDeleted == false);
                var resultItemUsed =
                        from s in itemUsed
                        group s by new { id = s.ItemId } into g
                        select new
                        {
                            ItemId = g.Key.id,
                            quantity = g.Sum(x => x.QuantityUsed)
                        };
                List<string> itemNames = new List<string>();
                foreach (var item in resultItemUsed.Select(x=>x.ItemId))
                {
                    var name = _unitOfWork.ItemRepository.GetSingle(x => x.ItemId == item).ItemName;
                    itemNames.Add(name);
                }
                hm.itemNames = itemNames;
                
                hm.itemUsed = resultItemUsed.Select(x => x.quantity.Value).ToList();
            }
            catch (Exception ex)
            {
                hm.monthlyIncome = 0;
                hm.totalIncome = 0;
            }
            return View(hm);
        }

        public int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
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