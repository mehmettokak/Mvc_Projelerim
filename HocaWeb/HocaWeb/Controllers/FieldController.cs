using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HocaWeb.Models;

namespace HocaWeb.Controllers
{
    public class FieldController : Controller
    {
        // GET: Field
        public ActionResult Works(int? sayfa,string katg)
        {
            Field wrk = new Field();
            wrk = Field.ResimGetir(sayfa, katg);
            return View("works", wrk);
        }
    }
}