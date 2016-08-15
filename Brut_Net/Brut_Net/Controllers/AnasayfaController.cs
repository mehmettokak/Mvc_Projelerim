using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Brut_Net.Models;

namespace Brut_Net.Controllers
{
    public class AnasayfaController : Controller
    {
        // GET: Anasayfa
        public ActionResult Net_Brut_Hesaplama()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Hesaplamalar(double netMaas)
        {
            if (netMaas < 1300) netMaas = 1300; //  *Asgari ücret
            Genel gnl = new Genel();
            gnl = Genel.Hesapla(netMaas);
            return View("Net_Brut_Hesaplama", gnl);
        }
        public ActionResult Net_maas()
        {
            return View();
        }
    }
}