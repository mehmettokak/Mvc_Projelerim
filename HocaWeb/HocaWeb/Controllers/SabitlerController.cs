using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HocaWeb.Models;
using System.IO;

namespace HocaWeb.Controllers
{
    public class SabitlerController : Controller
    {
        // GET: Sabitler
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult HomeGuncelle(int? sabit_id, string text_1, string text_2, string text_3, string text_4, string text_5, string text_6, string text_7, string text_8, string text_9, string Gelen)
        {
            using (var _db = new Models.AkademikEntities())
            {
                try
                {
                    _db.sp_mehmet_Home_Sabit_Guncelle(sabit_id, text_1, text_2, text_3, text_4, text_5, text_6, text_7, text_8, text_9, Gelen);
                    ViewBag.Error = "<span style='color: green; float:left;'>Kayıt Güncellendi.</span>";
                }
                catch (Exception)
                {
                    ViewBag.Error = "<span style='color: red; float:left; '>Kayıt Başarısız !!!.Lutfen Tekrar Deneyiniz ?</span>";
                }

            }
            if (Gelen == "HOME") { return View("Home"); }
            else { return View("Contact"); }
            
        }
        [HttpPost]
        public ActionResult H_Img_update(int? img_id, string img_name, string img_title, string img_url, HttpPostedFileBase RESIM)
        {

            string resim_yolu = "";
            string URUN_AD_db = "/images/Resimler/";
            if (RESIM != null && RESIM.ContentLength > 0)
            {
                try
                {
                    img_name = "Resim";
                    string resimAdi = adminGenel.SEO_URL(img_name) + "_" + DateTime.Now.ToString("dd-MM-yyyy--HH-mm-ss") + Path.GetExtension(RESIM.FileName);
                    resim_yolu = Path.Combine(Server.MapPath("~/images/Resimler"), resimAdi);
                    RESIM.SaveAs(resim_yolu);
                    URUN_AD_db += resimAdi;
                }
                catch (Exception)
                {

                }
            }
            using (var _db = new Models.AkademikEntities())
            {

                try
                {
                    if (URUN_AD_db == "/images/Resimler/")
                    {
                        string urun_resimAdi = null;
                        _db.sp_mehmet_HomeImage_Guncelle(img_id, img_name, img_title, img_url, urun_resimAdi);
                        ViewBag.Error = "<span style='color: green; float:right;'>Kayıt Başarılı</span>";
                    }
                    else
                    {

                        _db.sp_mehmet_HomeImage_Guncelle(img_id, img_name, img_title, img_url, URUN_AD_db);
                        ViewBag.Error = "<span style='color: green; float:right;'>Kayıt Başarılı</span>";
                    }

                }
                catch (Exception)
                {
                    ViewBag.Error = "<span style='color: red; float:right;'>Kayıt Başarısız</span>";
                }

            }

            return View("Home");

        }

        [HttpPost]
        public ActionResult UserPaswordGuncelle(int? id, string user, string pasword)
        {

            using (var _db = new Models.AkademikEntities())
            {
                try
                {
                    string sifrelenenPassword = Sabitler.Sifreolustur(pasword);
                    _db.sp_mehmet_user_pasword_update(id, user, sifrelenenPassword);
                    ViewBag.Error = "<span style='color: green; float:left;'>Kayıt Güncellendi.</span>";
                }
                catch (Exception)
                {
                    ViewBag.Error = "<span style='color: red; float:left; '>Kayıt Başarısız olmustur.Lutfen Tekrar Deneyiniz ?</span>";
                }

            }

            return View("Home");
        }

    }
}