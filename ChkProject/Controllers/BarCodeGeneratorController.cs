using ChkProject.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ChkProject.Controllers
{
    public class BarCodeGeneratorController : Controller
    {
        // GET: BarCodeGenerator
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GenerateBarCode(ProductModel model)
        {
            var BarCodeImage = "";
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (Bitmap bitmap = new Bitmap(model.ProductId.ToString().Length * 40, 80))
                {
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        Font oFont = new Font("IDAutomationHC39M", 16);
                        PointF point = new PointF(2f, 2f);
                        SolidBrush whiteBrush = new SolidBrush(Color.White);
                        graphics.FillRectangle(whiteBrush, 0, 0, bitmap.Width, bitmap.Height);
                        SolidBrush blackBrush = new SolidBrush(Color.DarkBlue);
                        graphics.DrawString("*" + model.ProductId.ToString()+DateTime.Now.Year + "*", oFont, blackBrush, point);
                    }
                    bitmap.Save( memoryStream, ImageFormat.Jpeg);
                    BarCodeImage = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
                    //var bID = RemoveSpecialCharacters(model.ProductName);
                    //string mynewpath = Request.PhysicalApplicationPath + "Upload\\";
                    //bitmap.Save(mynewpath + bID + ".jpg", ImageFormat.Jpeg);
                    //var image = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());

                }
            }

            return Json(BarCodeImage, JsonRequestBehavior.AllowGet);
        }

        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }

    }
}