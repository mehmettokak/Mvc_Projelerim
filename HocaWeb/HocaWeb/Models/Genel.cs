using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HocaWeb.Models;

namespace HocaWeb.Models
{
    public class Genel
    {

        public List<ResimUrl> resimUrl { get; set; }
        public List<Miscellaneous> miscellaneous { get; set; }
        public List<Miscellaneous_Icerik> MISC_Icerik { get; set; }
        public List<UrunListeleri> urun_listeleri { get; set; }
        public List<Kategori_Listeleri> Kategori_Listeleri { get; set; }
        /*****************************************************************************************/
        //public List<altmenu> ALTMENU { get; set; }
        public  string SuraYaz(DateTime zaman)
        {
             TimeSpan sure = DateTime.Now - zaman;
            string surehesapla="";
    if (sure.TotalMilliseconds < 0)
    {//ici bos olsun..sure - olursa
       surehesapla=" <div class=time-ago></div>";
    }
    else
    {
        if ((sure).TotalMinutes < 1)
        {
          surehesapla="  <div class=time-ago>Şimdi</div>";
        }
        else if (sure.TotalHours < 1)
        {//1 dk buyuk 1 saaten kucuk
          surehesapla="<div class=time-ago>"+sure.Minutes.ToString()+" Dakika Önce</div>";
        }
        else if (sure.TotalDays < 1)
        {
           surehesapla="<div class=time-ago>"+sure.Hours.ToString()+" Saat Önce</div>";
        }
        else
        {
            surehesapla="<div class=time-ago>"+zaman.ToString("dd.MM.yyyy")+"</div>";
        }

    }
    return surehesapla;
        }
        public List<UrunlerMenu> Urunler_menu
        {
            get
            {
                List<UrunlerMenu> L = new List<UrunlerMenu>();
                using (var DB = new AkademikEntities())
                {
                    foreach (var item in DB.sp_mehmettokak_nested_menu().Where(x => !string.IsNullOrEmpty(x.alt_ktgri_ad)))
                    {
                        UrunlerMenu Eleman = new UrunlerMenu();
                        Eleman.AltKtgoriadi = item.alt_ktgri_ad;
                        Eleman.AltKtgriId = (int)item.alt_ktgri_id;
                        Eleman.altkat_menu_id = (int)item.alt_ktgri_menu_id;
                        Eleman.Menu_id = item.menu_id;
                        Eleman.Menu_ad = item.menu_ad;
                        L.Add(Eleman);
                    }
                }
                return L;
            }
        }
        public List<iletisim> Iletisim
        {
            get
            {
                List<iletisim> L = new List<iletisim>();
                using (var DB = new AkademikEntities())
                {
                    foreach (var item in DB.sp_mehmet_iletisim_getir())
                    {
                        iletisim Eleman = new iletisim();
                        Eleman.iletisim_Id = item.iletisim_id;
                        Eleman.iletisim_Ad_Soyad = item.iletisim_ad_soyad;
                        Eleman.iletisim_Icerik = item.iletisim_icerik;
                        Eleman.email = item.iletisim_email;
                        Eleman.iletisim_konu = item.iletisim_konu;
                        Eleman.iletisim_durum =(bool) item.iletisim_durum;
                        Eleman.iletisim_tarih = (DateTime)item.iletisim_tarih;
                    
                        L.Add(Eleman);
                    }
                    
                }
                return L;
            }
        }
        
       
    }
    public class UrunlerMenu
    {
        public string AltKtgoriadi { get; set; }
       
        public int AltKtgriId { get; set; }
        public int Menu_id { get; set; }
        public string Menu_ad { get; set; }
        public int altkat_menu_id { get; set; }
    }
    public class UrunListeleri
    {
        public int UrunID { get; set; }
        public string UrunAdi { get; set; }
        public int Urun_Kategori_Id { get; set; }
       
    }
    public class Kategori_Listeleri
    {
        public int Kategori_ID { get; set; }
        public string  Kategori_AD { get; set; }
        public int Alt_Katgri_İd { get; set; }
    }
   
    public class urunler
    {
        public string urun_ad { get; set; }
        public string urun_kategorı_id { get; set; }
    }
 
    public class ResimUrl
    {


        public string Resim_Ktgr_adi { get; set; }
        public string Resim_Ktgr_No { get; set; }
    }
    public class Resim_Ekle
    {
        public string Resim_B_adi { get; set; }
        public string Resim_K_adi { get; set; }
        public string Resim_alt_text { get; set; }
        public int Resim_Ktgri_Id { get; set; }
    }
    public class Miscellaneous
    {
        public string Misc_Ktgr_adi { get; set; }
        public string Misc_Ktgr_soyadi { get; set; }
    }
    public class Miscellaneous_Icerik
    {
        public string Misc_Icerik_Tarih1 { get; set; }
        public string Misc_Icerik_Tarih2 { get; set; }
        public string Misc_Icerik_Baslik { get; set; }
        public string Misc_Icerik_Text { get; set; }
    }
    public class iletisim
    {
        public int iletisim_Id { get; set; }
        public string iletisim_Ad_Soyad { get; set; }
        public string iletisim_Icerik { get; set; }
        public string email { get; set; }
        public string iletisim_konu { get; set; }
        public bool iletisim_durum { get; set; }
        public DateTime iletisim_tarih { get; set; }
       
     
    }
}

