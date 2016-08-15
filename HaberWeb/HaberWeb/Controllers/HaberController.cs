using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HaberWeb.Models;

namespace HaberWeb.Controllers
{
    public class HaberController : Controller
    {
        Model1 DB = new Model1();

        // GET: Haber 
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HaberGoster(int id)
        {
            return View(DB.Haber.FirstOrDefault(x => x.H_id == id));
        }
        [HttpPost]
        public ActionResult YorumYap(string txtAdSoyad, string txtMail, string txtIcerik, int txtH_id)
        {
            H_Yorum Yorum = new H_Yorum();
            Yorum.H_Y_AdSoyad = txtAdSoyad;
            Yorum.H_Y_Mail = txtMail;
            Yorum.H_Y_Icerik = txtIcerik;
            Yorum.H_Y_HaberID = txtH_id;
            Yorum.H_Y_YayimTarihi = DateTime.Now;
            Yorum.H_Y_Onay = true;
            Yorum.H_Y_Ip = Request.ServerVariables["REMOTE_ADDR"];//ip bilgisini tutar.


            DB.H_Yorum.Add(Yorum); // Yorum nesnesini databasedeki H_Yorum Tablosuna ekle
            DB.SaveChanges();//db ye gonderilen bilgileri kaydet.
            return RedirectToAction("HaberGoster", new { id = txtH_id });//gosterilecek olan sayfayı gelen ıd ye gore sayfa gozukecek.id habergoster dekı ıd dir.
        }
        public int YorumBegendim(int id) //anlık degısım ıcın gerıye ınt donen meto olaacak.
        {

            H_Yorum yrm = DB.H_Yorum.FirstOrDefault(x => x.H_Y_ID == id);
            int artr_bgn = yrm.H_Y_Begendim + 1;
            yrm.H_Y_Begendim = artr_bgn;
            int toplam = yrm.H_Y_Begendim - yrm.H_Y_Begenmedim;
            DB.SaveChanges();
            return toplam;
        }
        public int YorumBegenmedim(int id)
        {
            H_Yorum yrm = DB.H_Yorum.FirstOrDefault(x => x.H_Y_ID == id);
            int bgn_artr = yrm.H_Y_Begenmedim + 1;
            yrm.H_Y_Begenmedim = bgn_artr;
            int toplam = yrm.H_Y_Begendim - yrm.H_Y_Begenmedim;
            DB.SaveChanges();
            return toplam;
        }

        public ActionResult KategoriEkle()
        {
            return View(DB.H_Kategori);
        }
    }
}
