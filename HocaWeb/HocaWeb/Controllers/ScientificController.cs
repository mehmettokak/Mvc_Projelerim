using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HocaWeb.Models;

namespace HocaWeb.Controllers
{
    public class ScientificController : Controller
    {
        // GET: Scientific
        public ActionResult Work(int? sayfa,string katg)
        {
            Scientific altmnu = new Scientific();
            altmnu = Scientific.altmenuGetir(sayfa, katg);
            return View("Work", altmnu);
        }
    }
}