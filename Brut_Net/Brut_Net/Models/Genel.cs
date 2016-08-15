using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Brut_Net.Models;

namespace Brut_Net.Models
{
    public class Genel : GenelToplam
    {
       
        public List<Bilgiler> tutulanBilgiler { get; set; }
        public List<GenelToplam> ToplamBilgiler { get; set; }
        public static Genel Hesapla(double netmaas)
        {
            string[] ay = { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };
            Genel model = new Genel();
            List<Bilgiler> Bilgiler = new List<Bilgiler>();

            for (int i = 0; i <= 11; i++)
            {

                Bilgiler bilgi = new Bilgiler();
 
                bilgi.NetUcret = netmaas;
                bilgi.BrutUcret = bilgi.NetUcret + bilgi.NetUcret * 40 / 100;
                bilgi.SskIsci = bilgi.BrutUcret * 14 / 100;
                bilgi.IssizlikIsci = bilgi.BrutUcret / 100;
                double vergilenecek_tutar = (bilgi.BrutUcret - bilgi.BrutUcret * 15 / 100);
                bilgi.GelirVergisi = vergilenecek_tutar * 15 / 100;
                bilgi.DamgaVergisi = bilgi.BrutUcret * 7.59 / 1000;//damga vergisi brut ucretin binde 7.59 dur.
                bilgi.GvMatrahi = bilgi.BrutUcret - bilgi.BrutUcret * 15 / 100;//brut ücretten ssk kesintisinin cıkarılması ıle bulundu.

                if (i > 0)
                {
                    bilgi.Kgvm = bilgi.GvMatrahi;
                    bilgi.Kgvm += Bilgiler[i - 1].Kgvm;
                }
                bilgi.Agi = 123;
                bilgi.NetUcret = bilgi.NetUcret + bilgi.Agi;

                bilgi.Aylar = ay[i];

                Bilgiler.Add(bilgi);

            }
            model.tutulanBilgiler = Bilgiler;

            List<GenelToplam> Toplam = new List<GenelToplam>();
            GenelToplam toplam = new GenelToplam();
            for (int i = 0; i <= 11; i++)
            {
                toplam.ToplamAgi += Bilgiler[i].Agi;
                toplam.ToplamBrutucret += Bilgiler[i].BrutUcret;
                toplam.ToplamGelirvergisi += Bilgiler[i].GelirVergisi;
                toplam.ToplamGvmatrahi += Bilgiler[i].GvMatrahi;
                toplam.ToplamIssizlikisci += Bilgiler[i].IssizlikIsci;
                toplam.ToplamKgvm += Bilgiler[i].Kgvm;
                toplam.ToplamNetucret += Bilgiler[i].NetUcret;
                toplam.ToplamSskisci += Bilgiler[i].SskIsci;
                toplam.ToplamDamgaVergisi += Bilgiler[i].DamgaVergisi;
            }

            Toplam.Add(toplam);
            model.ToplamBilgiler = Toplam;

            return model;
        }

    }
}