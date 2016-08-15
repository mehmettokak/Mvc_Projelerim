using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HocaWeb.Models;

namespace HocaWeb.Models
{
    public class miscellaneous:Genel
    {
        public static miscellaneous miscellaneousGetir(int? sayfa, string katg)
        {
            miscellaneous model = new miscellaneous();

            if (sayfa == null || sayfa < 1)
            {
                sayfa = 1;
            }

            if (string.IsNullOrEmpty(katg))
            {
                katg = "hepsi";
            }

            using (var _dbe = new AkademikEntities())
            {
                if (katg == "hepsi")
                {
                    List<Kategori_Listeleri> K_L = new List<Kategori_Listeleri>();
                    foreach (var item in _dbe.sp_mehmet_kategorileri_cek())
                    {
                        Kategori_Listeleri eleman = new Kategori_Listeleri();
                        eleman.Kategori_AD = item.kategori_ad;
                        eleman.Kategori_ID = item.kategori_id;

                        K_L.Add(eleman);
                    }
                    model.Kategori_Listeleri = K_L;

                    List<UrunListeleri> L = new List<UrunListeleri>();
                    foreach (var item in _dbe.sp_mehmettokak_urunliste_cek())
                    {
                        UrunListeleri Eleman = new UrunListeleri();
                        Eleman.UrunAdi = item.urun_ad;
                        Eleman.UrunID = item.urun_id;

                        L.Add(Eleman);
                    }
                    model.urun_listeleri = L;
                }
                else
                {
                    List<Kategori_Listeleri> K_L = new List<Kategori_Listeleri>();
                    List<UrunListeleri> L = new List<UrunListeleri>();
                    foreach (var item in _dbe.sp_mehmet_altkategoriye_gore_kategori_cek(katg))
                    {
                        Kategori_Listeleri eleman = new Kategori_Listeleri();
                        eleman.Kategori_AD = item.kategori_ad;
                        eleman.Kategori_ID = (int)item.kategori_id;
                        K_L.Add(eleman);
                        foreach (var item2 in _dbe.sp_mehmet_urunler_kategoriye_gore_liste_cek(item.kategori_id.ToString()))
                        {

                            UrunListeleri Eleman = new UrunListeleri();
                            Eleman.UrunAdi = item2.urun_ad;
                            Eleman.UrunID = item2.urun_id;
                            Eleman.Urun_Kategori_Id = item2.kategori_id;
                            L.Add(Eleman);
                        }
                        model.urun_listeleri = L;
                    }

                    model.Kategori_Listeleri = K_L;


                }

                return model;
            }
        }
    }
}