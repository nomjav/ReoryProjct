using ChkProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chakwal.Data;
using Chakwal.Data.Helpers;
using Chakwal.Data.MemberShip;
using Chakwal.Data.Repository;
using Chakwal.Data.Data;
using System.Globalization;
using System.Text;
using System.Data.Entity.Infrastructure;

namespace ChkProject.Controllers
{
    public class ReportController : Controller
    {
        UnitOfWork _unitOfWork = new UnitOfWork();
        // GET: Report
        public ActionResult Index()
        {
            return View("Index");
        }

        #region StockIn Report
        public ActionResult StockInReport()
        {
            //if (!PermissionService.Authorize(PermissionProvider.AccessManagementReports))
            //    return AccessDeniedView();
            var item = new SelectListItem { Text = "All", Value = "0", Selected = true };
            var list = _unitOfWork.ProductRepository.Get(x=>x.IsDeleted == false).Select(x => new SelectListItem
            {
                Text = x.ProductName,
                Value = x.ProductId.ToString()
            }).ToList();
            list.Insert(0, item);
            ViewBag.Product = list;
            StockInItemModel model = new StockInItemModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult StockInReport(StockInItemModel model)
        {
            try
            {
                var item = new SelectListItem { Text = "All", Value = "0", Selected = true };
                var list = _unitOfWork.ProductRepository.Get(x => x.IsDeleted == false).Select(x => new SelectListItem
                {
                    Text = x.ProductName,
                    Value = x.ProductId.ToString()
                }).ToList();
                list.Insert(0, item);
                ViewBag.Product = list;
                var sDate = model.FromDate;
                var eDate = model.ToDate;
                int pId = model.ProductId;
                var products = _unitOfWork.SP_StockInRepository.context.SP_StockIn(sDate.Value, eDate.Value, pId).ToList();

                List<StockInItemModel> ListModel = new List<StockInItemModel>();
                foreach (var product in products)
                {
                    StockInItemModel models = new StockInItemModel();

                    models.ProductName = product.ProductName;
                    models.Quantity = product.Quantity;
                    models.CompanyName = product.CompanyName;


                    ListModel.Add(models);
                }
                model.ReportGenerated = true;
                model.StockInItemList = ListModel;

                return View(model);
            }
#pragma warning disable
            catch (Exception exception)
            {
                //Logger.LogException(exception.Message, exception);
            }

              return View();
        }

        #endregion

        #region StockOut Report
        public ActionResult StockOutReport()
        {
            //if (!PermissionService.Authorize(PermissionProvider.AccessManagementReports))
            //    return AccessDeniedView();

            StockOutModel model = new StockOutModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult StockOutReport(StockOutModel model)
        {
            try
            {
                var sDate = model.FromDate;
                var eDate = model.ToDate;
                var products = _unitOfWork.SP_StockOutRepository.context.SP_StockOut(sDate.Value, eDate.Value).ToList();

                List<StockOutModel> ListModel = new List<StockOutModel>();
                foreach (var product in products)
                {
                    StockOutModel models = new StockOutModel();

                    models.ProductName = product.ProductName;
                    models.Quantity = product.Quantity;
                    models.CompanyName = product.BranchName;
                    models.Price = product.TotalPrice;



                    ListModel.Add(models);
                }
                model.ReportGenerated = true;
                model.StockOutModelList = ListModel;

                return View(model);
            }
#pragma warning disable
            catch (Exception exception)
            {
                //Logger.LogException(exception.Message, exception);
            }

            return View();
        }

        #endregion
    }
}


