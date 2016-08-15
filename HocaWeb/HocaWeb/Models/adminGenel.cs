using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using HocaWeb.Controllers;

namespace HocaWeb.Models
{
    public class adminGenel
    {
        public static string SEO_URL(string URL)
        {
            string seourl = "";
            seourl = URL.Trim();
            seourl = seourl.Replace("ğ", "g");
            seourl = seourl.Replace("Ğ", "G");
            seourl = seourl.Replace("ü", "u");
            seourl = seourl.Replace("Ü", "U");
            seourl = seourl.Replace("ş", "s");
            seourl = seourl.Replace("Ş", "S");
            seourl = seourl.Replace("ı", "i");
            seourl = seourl.Replace("İ", "I");
            seourl = seourl.Replace("ö", "o");
            seourl = seourl.Replace("Ö", "O");
            seourl = seourl.Replace("ç", "c");
            seourl = seourl.Replace("Ç", "C");
            seourl = seourl.Replace("-", "+");
            seourl = seourl.Replace(" ", "+");
            seourl = seourl.Trim();
            seourl = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9+]").Replace(seourl, "");
            seourl = seourl.Trim();
            seourl = seourl.Replace("+", "-");
            seourl = seourl.ToLower();
            string seourlson = "";
            string[] a = seourl.Split('-');
            seourlson = string.Join("-", a.Where(x => !string.IsNullOrEmpty(x)));
            return seourlson;
        }
        public static kullanici girisKontrol(string F_user,string F_pass)
        {
            kullanici kl = new kullanici();
            kl.K_durum = false;
            using (var _db=new AkademikEntities())
            {
                foreach (var item in _db.sp_mehmet_giris_kontrol(F_user,F_pass))
                {
                    if (!string.IsNullOrEmpty(item.kullanici_adi))
                    {
                        kl.K_durum = true;
                        HttpContext.Current.Session["user"] = item.kullanici_adi;
                        //kl.K_adi = item.kullanici_adi;
                        //kl.K_sifre = item.kullanici_sifre;
                    }
                }
            }
            return kl;

        }

        public List<Menu_Kategorileri> MenuKategori
        {
            get
            {
                List<Menu_Kategorileri> L = new List<Menu_Kategorileri>();
                using (var DB = new AkademikEntities())
                {
                    foreach (var item in DB.sp_mehmet_Menu_Getir().Where(x => !string.IsNullOrEmpty(x.menu_ad)))
                    {
                        Menu_Kategorileri Eleman = new Menu_Kategorileri();
                     
                        Eleman.MenuId = item.menu_id;
                        Eleman.MenuAd = item.menu_ad;
                        L.Add(Eleman);
                    }
                }
                return L;
            }
        }

        public List<altkategori> Altkategoriler
        {
            get
            {
                List<altkategori> L = new List<altkategori>();
                using (var DB = new AkademikEntities())
                {
                    foreach (var item in DB.sp_mehmettokak_nested_menu().Where(x => !string.IsNullOrEmpty(x.alt_ktgri_ad)))
                    {
                        altkategori Eleman = new altkategori();
                        Eleman.altKtgoriadi = item.alt_ktgri_ad;
                        Eleman.altKtgriId = (int)item.alt_ktgri_id;
                        Eleman.altkat_menuIid = (int)item.alt_ktgri_menu_id;
                        Eleman.menu_id = item.menu_id;
                        Eleman.menu_ad = item.menu_ad;
                        L.Add(Eleman);
                    }
                }
                return L;
            }
        }
        public List<Kategori> Kategoriler
        {
            get
            {
                List<Kategori> L = new List<Kategori>();
                using (var DB = new AkademikEntities())
                {
                    foreach (var item in DB.sp_mehmet_kategorileri_cek().Where(x => !string.IsNullOrEmpty(x.kategori_ad)))
                    {
                        Kategori Eleman = new Kategori();
                        Eleman.Kategoriadi = item.kategori_ad;
                        Eleman.KategoriId = (int)item.kategori_id;
                        Eleman.Kategori_altktgri_id = (int)item.kategori_altktgr_id;
                      
                        L.Add(Eleman);
                    }
                }
                return L;
            }
        }
        public List<Urunlerim> Urunlerim
        {
            get
            {
                List<Urunlerim> L = new List<Urunlerim>();
                using (var DB = new AkademikEntities())
                {
                    foreach (var item in DB.sp_mehmettokak_urunliste_cek().Where(x => !string.IsNullOrEmpty(x.urun_ad)))
                    {
                        Urunlerim Eleman = new Urunlerim();
                        Eleman.Urunadi = item.urun_ad;
                        Eleman.UrunId = (int)item.urun_id;
                        Eleman.UrunktgriId = (int)item.urun_kategori_id;
                        Eleman.UrunAcıklama = item.urun_aciklama;

                        L.Add(Eleman);
                    }
                }
                return L;
            }
        }
      
    }
    public class kullanici
    {
        public string K_adi  { get; set; }
        public string  K_sifre { get; set; }
        public bool  K_durum { get; set; }
    }

    public class Menu_Kategorileri
    {
        public string MenuAd { get; set; }
       
        public int MenuId { get; set; }
        public string MenuUrl { get; set; }
      
    }
    public class altkategori
    {
        public string altKtgoriadi { get; set; }

        public int altKtgriId { get; set; }
        public int menu_id { get; set; }
        public string menu_ad { get; set; }
        public int altkat_menuIid { get; set; }
    }
    public class Kategori
    {
        public string Kategoriadi { get; set; }

        public int KategoriId { get; set; }
        public int altKategoriId { get; set; }
        public string altKategoriAd { get; set; }
        public int Kategori_altktgri_id { get; set; }
    }
    public class Urunlerim
    {
        public string Urunadi { get; set; }

        public int UrunId { get; set; }
        public int UrunktgriId { get; set; }
        public string kategoriad { get; set; }
        public int altktgri_id { get; set; }
        public string  UrunAcıklama { get; set; }
    }

}