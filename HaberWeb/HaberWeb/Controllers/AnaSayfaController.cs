using HaberWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HaberWeb.Controllers
{
    public class AnaSayfaController : Controller
    {
        // GET: AnaSayfa
        Model1 DB = new Model1();
        public ActionResult AnaSayfa()
        {
            return View();
        }
        public ActionResult YeniHaberGetir()
        {
            var yeniHAber = DB.Haber;
            return View(yeniHAber);
        }
        public ActionResult SliderGetir()
        {//HAber Tablosundan -->haber tip tablosunda baglan,habertip tablosundan tip adı=manset olanı yayım tarıhıne gore tersten 10 tanesini al
            var haberler = DB.Haber.Where(x => x.H_Tip.H_T_Adi == "Manset").OrderByDescending(x => x.H_YayimTarihi).Take(10);
            return View(haberler);
            //modelime haberler nesnesini göndermiş oldum
        }
        public ActionResult EnSonHaberler()
        {
            //Son haberleri ayırt etmeden manset haberleri vb ayırt etmeden karısık son haberlerı getırıyoruz.
            var karisikhaber = DB.Haber.OrderByDescending(x => x.H_YayimTarihi).Take(7);
            return View(karisikhaber);
        }
        public ActionResult UstTabHaberler()
        {
            return View(); 
        }
        public ActionResult Tab_VideoGetir()
        {
            var videolar = DB.Haber.Where(x => x.H_TipID == 2 && x.H_VideoYol != null).OrderByDescending(x => x.H_id).Take(8);
            return View(videolar);
        }
        public ActionResult Tab_GorusGetir()
        {
            var gorusler = DB.H_Gorusler.OrderByDescending(x => x.H_G_Tarih).Take(2);
            return View(gorusler);
        }
        public ActionResult Tab_AnketGetir()
        {//anket ıd cookie ye atıcaz.

           if( Session["oylanananket"] != null)
           { //***Anket oylanmıssa yanı session null degilse..oylama yapılmıssa anket sonucu gosteren uyg gostermelıyız.
               return View("Tab_AnketSonucGetir",Session["oylanananket"]);//model de gondermemız gerekıyor.
           }
           else
           {//anket oylanmamıssa

            HttpCookie anketckie = Request.Cookies["anketler"];
            if (anketckie == null)
            {
                anketckie = new HttpCookie("anketler");
            }
            string anketcookie = anketckie.Value;
            if (anketcookie == null)
            {
                anketcookie = "0";
            }

            //cookies e anket tablomu getir.anketcookies ata.(cookies string turundedir.) cookie içindeki elemanları diziye atayoruz. , karakterine bolerek.(1,2,3,4,5 gibi tutacak cookies)
            int[] oylananlar = anketcookie.Split(',').Select(x => Convert.ToInt32(x)).ToArray();//selectle herbir x elemanını int cevirdik.ve listelettık arraylıstle
            //anketlerimi getirecem.
            var anketler = DB.H_Anket.Where(x => x.H_Anket_Aktifmi == true && x.H_Anket_SonOyTarihi >= DateTime.Now && !oylananlar.Contains(x.H_Anket_ID)).ToList();
            //Anket tablosunda aktif olanı ve oylananlar dızısinde Anket_id içermeyenleri ve id yi stringe cevirdik.icermeyenler olsun kı kullanıcı karsınıa farklı anket cıksın .aynı anket cıkmasın.cookie ıcınde icermeyen anket ıd bulduk..bunuda randomlayıp rastgele karsına sunacaz.,Sonoylamatarihi bugunden kusuck veya eşit se getir.buyukse sursı dolmustur getirmez.
            if (anketler.Any())
            { //Any varmı yokmu..anketler varsa true yoksa false doner.anketler varsa random işlemi yap demek ıstenıyor.
                Random Rastgele = new Random();
                return View(anketler[Rastgele.Next(0, anketler.Count)]);
                //modelimze anketler dizisinde eleman yazıcagımız zaman anketler objesının sonuna tolist veya arraylist yazmamız lazım,
            }
           
                return View("AnketYok");
            
        }
        }
        public ActionResult OyVer(int id)
        {//1.Anketid ye gore oysayısı artıracaz.2.Verilen oyu alıp katılımcı  sayısını ve oysayısını artıracak.3. cookie gidip diyecekki su ıd oy verildi diyecek.
            int secenekid = Convert.ToInt32(Request.Form["choice"]);//form dan choice adli degeri al(bu deger secenekid dir.) seneckid ye ata.
            var anket = DB.H_Anket.FirstOrDefault(x=>x.H_Anket_ID==id);//firstordefoult tabloda ıstenılenı ceker.whre gıbı-dır.where ile farkı ıstenılen yoksa ılkını getırır.
            var secenek = DB.H_Anket_Secenek.FirstOrDefault(x=>x.H_A_S_ID==secenekid); //H_Anket_Secenek yerine var da kullanabılırız.
            anket.H_ANket_KatilimciSayisi++;
            secenek.H_A_S_OySayisi++;
            DB.SaveChanges();//artırma ıslemlerını verıtabanına yaz.kaydet.
            HttpCookie anketcookie = Request.Cookies["anketler"];
            if (anketcookie!=null) //anketcookie varsa yanı doluysa yanı bos degılse..varsa ıcındekı degeri yanına virgul ekleyerek cookie eklesın.haberversın bu ankette oy kullandı dıye.
            {
                anketcookie.Value += "," + id;
              
            }
            else
            {//anketcookie bossa ,yoksa yenı bır httpcookie olusturacaz.
                anketcookie = new HttpCookie("anketler");
                anketcookie.Value = "0"; 
                //boş veya null olursa hatalıdır.yanı yukarılarda convertoint32(x) x null veya bos olursa donusumde hata alır.onlemek ıcın 0 yazıyoruz.varsayılan sıfır olacaktır.0,1,2,3,5,6,8 gibi devam edecektir cookiede.
            } 
            //bunu onceden tanımladııgımız anketler cookies icine ekle.
            anketcookie.Expires = anket.H_Anket_SonOyTarihi.AddDays(1);
           HttpContext.Response.Cookies.Add(anketcookie); //oy verme işlemi anket cookie eklendi.bundan sonra aynı ankete bır daha cevap veremıcek
           Session["oylanananket"] = anket;//***oy kullanılan anketi session da tutalım.controllerdan anketgetir e gideriz.orda session null degilse anket getirsin diyecez.
            return View("AnaSayfa");
        }

        public ActionResult KucukFotoSliderGetir()
        {
            var KucukFotoHaber = DB.Haber.Where(x => x.H_Tip.H_T_Adi =="Galeri").OrderByDescending(x=>x.H_YayimTarihi).Take(6);
            return View(KucukFotoHaber);
        }

        //------------------------------------------------
        public ActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Giris(string txtK_Adi, string txt_Sifre, string beni_hatirla, string returnurl = "")//returnpageurl varsayılan bos gelsın
        {
        //    if (Membership.ValidateUser(txtK_Adi, txt_Sifre))/*k_adi ve parola membershıpte varmı varsa true doner.*/
        //    {
        //        bool b_h = Convert.ToBoolean(beni_hatirla);
        //        FormsAuthentication.RedirectFromLoginPage(txtK_Adi, b_h);//kullanıcı adına gore giris yap,checktax tıklanırsa hatırlasın mı--cookie de saklansınmı yazarsak ve tıklanırlırsa saklanır.
        //        if (!string.IsNullOrEmpty(returnurl))//returnpageurl bos veya null degılse
        //        {
        //            return Redirect(returnurl);
        //        }
        //        else
        //        {//bos gelirse yonetim sayfasına gır.
        //            return RedirectToAction("Index", "Yonetim");
        //        }
              
        //    }
          
         //return View();
            if (txtK_Adi == "mehmet" && txt_Sifre == "tokak")
            {
                return View("Index", "Yonetim");
            }
            else
            {
                return View();//if teki kod true donmedıyse kendı sayfasını tekrardan doner.
            }
            
        }
    }
}