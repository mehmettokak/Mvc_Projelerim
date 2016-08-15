using HaberWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaberWeb.Controllers
{
    public class AramaController : Controller
    {
        // GET: Arama
       
        public ActionResult Ara(string txtArama)
        {
            return View();
        }
    }
}