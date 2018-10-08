using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChkProject.Controllers
{
    [Authorize]
    public class MaterialController : Controller
    {
        // GET: Item
        public ActionResult Index()
        {
            return View();
        }
    }
}