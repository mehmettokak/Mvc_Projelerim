using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HocaWeb.Models;

namespace HocaWeb.Controllers
{
    public class MiscellaneousController : Controller
    {
        // GET: Miscellaneous
        public ActionResult Miscellaneous(int?sayfa,string katg)
        {
            miscellaneous msc = new miscellaneous();
            msc = miscellaneous.miscellaneousGetir(sayfa, katg);
            return View("Miscellaneous", msc);
        }
    }
}