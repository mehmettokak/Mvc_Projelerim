using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HocaWeb.Models;
using System.IO;

namespace HocaWeb.Controllers
{
    public class AdminController : Controller
    {
        kullanici KL = new kullanici();
        
        // GET: Admin
        public ActionResult giris()
        {
            return View();
        }
        public ActionResult altkategori()
        {
            return View();
        }
        public ActionResult kategori()
        {
            return View();
        }
        public ActionResult urunler()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
        public PartialViewResult toplammail()
        {
            return PartialView();
        }
        public ActionResult Mesajlar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GirisKontrol(string f_User, string f_pass)
        {
            string sifreliPassword = Sabitler.Sifreolustur(f_pass);
            KL = adminGenel.girisKontrol(f_User, sifreliPassword);
            ViewBag.isim = KL.K_adi;
            if (KL.K_durum)//durum true ise
            {
                return RedirectToAction("Page");//durum true ise page sayfasına gı KL bılgılerını page sayfanıa gonder.ad  bılgısı o sayfaya gidecek
            }
            else
            {
                ViewBag.Error = "<span style='color: red; float:right;'>Kullanıcı Adı veya Şifre Yanlış</span>";
                return View("giris");
            }

        }
        public ActionResult AdminPage()
        {
            //if (Session["user"] == null)
            //    return RedirectToAction("giris");
            
            return View();
        }
//-------------------------------Alt Kategori---------------------------------------------
        [HttpPost]
        public ActionResult AltKategoriEkle(string ad, int kat_ID)
        {
            using (var _db = new Models.AkademikEntities())
            {
               if(kat_ID>0&!string.IsNullOrEmpty(ad))
                {
                    _db.sp_mehmet_altkategori_ekle(ad, kat_ID);
                    ViewBag.Error = "<span style='color: green; float:left;'>Kayıt Veritabanına Eklenmiştir.</span>";
                }
                else
                {
                    ViewBag.Error = "<span style='color: red; float:left; '>Kayıt Başarısız olmustur.Lutfen Tekrar deneyiniz ?</span>";
                }

            }

            return View("altkategori");
        }
          [HttpPost]
        public ActionResult AltKategoriGuncelle(int? altkatgriId, string altktgradi, int? altkatgriMenuId)
        {
            using (var _db = new Models.AkademikEntities())
            {
                try
                {
                    _db.sp_mehmet_altkategori_Guncelle(altkatgriId, altktgradi, altkatgriMenuId);
                    ViewBag.Error = "<span style='color: green; float:left;'>Kayıt Güncellendi.</span>";
                }
                catch(Exception)
                {
                    ViewBag.Error = "<span style='color: red; float:left; '>Kayıt Başarısız olmustur.Lutfen Tekrar Deneyiniz ?</span>";
                }

            }

            return View("altkategori");
        }
          [HttpPost]
          public ActionResult AltKategoriSil(int altkatgriId)
          {
              using (var _db = new Models.AkademikEntities())
              {
                  try
                  {
                      _db.sp_mehmet_altkategori_Sil(altkatgriId);
                      ViewBag.Error = "<span style='color: green; float:left;'>Kayıt Silme İşlemi Başarıylea  Gerçekleşmistir.</span>";
                  }
                  catch (Exception)
                  {
                      ViewBag.Error = "<span style='color: red; float:left; '>Kayıt Silme İşlemi Başarısız olmustur.Lutfen Tekrar deneyiniz ?</span>";
                  }

              }

              return View("altkategori");
          }
        //------------------------------------Kategori-----------------------------------------
          [HttpPost]
          public ActionResult KategoriEkle(string ad, int kat_ID)
          {
              using (var _db = new Models.AkademikEntities())
              {
                  if (kat_ID > 0 & !string.IsNullOrEmpty(ad))
                  {
                      _db.sp_mehmet_Kategori_ekle(ad, kat_ID);
                      ViewBag.Error = "<span style='color: green; float:left;'>Kayıt Veritabanına Eklenmiştir.</span>";
                  }
                  else
                  {
                      ViewBag.Error = "<span style='color: red; float:left; '>Kayıt Başarısız olmustur.Lutfen Tekrar deneyiniz ?</span>";
                  }

              }

              return View("kategori");
          }
          [HttpPost]
          public ActionResult KategoriGuncelle(int? ktgriId, string ktgradi, int? Kat_altkatgriId)
          {
              using (var _db = new Models.AkademikEntities())
              {
                  try
                  {
                      _db.sp_mehmet_Kategori_Guncelle(ktgriId, ktgradi, Kat_altkatgriId);
                      ViewBag.Error = "<span style='color: green; float:left;'>Kayıt Güncellendi.</span>";
                  }
                  catch (Exception)
                  {
                      ViewBag.Error = "<span style='color: red; float:left; '>Kayıt Başarısız !!!.Lutfen Tekrar Deneyiniz ?</span>";
                  }

              }

              return View("kategori");
          }
          [HttpPost]
          public ActionResult KategoriSil(int ktgriId)
          {
              using (var _db = new Models.AkademikEntities())
              {
                  try
                  {
                      _db.sp_mehmet_kategori_Sil(ktgriId);
                      ViewBag.Error = "<span style='color: green; float:left;'>Kayıt Silme İşlemi Başarıylea  Gerçekleşmistir.</span>";
                  }
                  catch (Exception)
                  {
                      ViewBag.Error = "<span style='color: red; float:left; '>Kayıt Silme İşlemi Başarısız olmustur.Lutfen Tekrar deneyiniz ?</span>";
                  }

              }

              return View("kategori");
          }
        //------------------------------------------------Urunler-----------------------------------------------------------
          [HttpPost]
          public ActionResult UrunEkleGuncelle(int? URUN_ID, string URUN_AD, string URUN_ACIKLAMA, int URUN_KTGRI_ID, HttpPostedFileBase RESIM)
          {

              string resim_yolu = "";
              string URUN_AD_db = "/images/Resimler/";
              if (RESIM != null && RESIM.ContentLength > 0)
              {
                  try
                  {
                      URUN_AD = "Resim";
                      string resimAdi = adminGenel.SEO_URL(URUN_AD) + "_" + DateTime.Now.ToString("dd-MM-yyyy--HH-mm-ss") + Path.GetExtension(RESIM.FileName);
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
                      { string urun_resimAdi = null;
                      _db.sp_mehmet_urun_Ekle_Guncelle(URUN_ID, URUN_AD, URUN_ACIKLAMA, URUN_KTGRI_ID, urun_resimAdi);
                      ViewBag.Error = "<span style='color: green; float:right;'>Kayıt Başarılı</span>";
                      }
                      else
                      {

                          _db.sp_mehmet_urun_Ekle_Guncelle(URUN_ID, URUN_AD, URUN_ACIKLAMA, URUN_KTGRI_ID, URUN_AD_db);
                          ViewBag.Error = "<span style='color: green; float:right;'>Kayıt Başarılı</span>";
                      }
                    
                  }
                  catch (Exception)
                  {
                      ViewBag.Error = "<span style='color: red; float:right;'>Kayıt Başarısız</span>";
                  }

              }

              return View("urunler");

          }
          [HttpPost]
          public ActionResult UrunSil(int URUN_ID)
          {
              using (var _db = new Models.AkademikEntities())
              {
                  try
                  {
                      _db.sp_mehmet_Urun_Sil(URUN_ID);
                      ViewBag.Error = "<span style='color: green; float:left;'>Kayıt Silme İşlemi Başarıylea  Gerçekleşmistir.</span>";
                  }
                  catch (Exception)
                  {
                      ViewBag.Error = "<span style='color: red; float:left; '>Kayıt Silme İşlemi Başarısız olmustur.Lutfen Tekrar deneyiniz ?</span>";
                  }

              }

              return View("urunler");
          }
        public ActionResult Logout()
        {
            Session["user"] = null;
                return RedirectToAction("giris");

            
        }

    }
}