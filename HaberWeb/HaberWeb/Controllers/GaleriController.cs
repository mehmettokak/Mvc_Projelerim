using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HaberWeb.Models;

namespace HaberWeb.Controllers
{
    public class GaleriController : Controller
    {
        Model1 DB = new Model1();
        // GET: Galeri
        public ActionResult Index()
        {
            var habergaleri = DB.Haber.Where(x=>x.H_Tip.H_T_Adi=="Galeri").OrderByDescending(x=>x.H_YayimTarihi);
            return View(habergaleri);
        }
        public ActionResult GaleriGoruntule(int id)
        {
            var galerihaber = DB.Haber.FirstOrDefault(x=>x.H_id==id);
        return View(galerihaber);
        }
    }
}