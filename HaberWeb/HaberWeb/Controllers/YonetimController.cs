using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HaberWeb.Models;
using System.Web.Security;
using System.Drawing;
using System.IO;

namespace HaberWeb.Controllers
{
    //[Authorize]
    public class YonetimController : Controller
    {
        Model1 DB = new Model1();
        // GET: Yonetim
       //[Authorize]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HaberYonet()
        {
            return View();
        }
        public ActionResult HaberEkle()
        {                       //kategorilei selecliste koy,valuemember olarak ıd sını yanı ıd alanı secıldıgı zaman gonder,k_adi alanınıda goster..yanı select liste kategorı tablosundan secılen ıd ye gore adını goster demıs olduk
            ViewBag.KATEGORIID = new SelectList(DB.H_Kategori, "H_K_ID", "H_K_Adi");//value si ne id leri ve icine de adını yazar.id arkaplanda adı ıse dırek gozukur.
            ViewBag.TIPID = new SelectList(DB.H_Tip, "H_T_ID", "H_T_Adi");//ip tablsounu ıd ye gore adını sun.
            ViewBag.YAZARID = new SelectList(DB.H_Yazar, "H_Y_ID", "H_Y_Adi");
            return View();
        }
    
        string ResimKaydet(HttpPostedFileBase fileResim)//httppostedfile tipinde parametre alır.Resimleri kaydederken Resimler dıye bir klasor olusturuyorz.
      { //Gelen resimi image tipinde almamız gerekiyor.bu tip sınıfı using System.Drawing; dadır.
          //InputStream==>gelen resmi 1 0 lar olarak alır.paracıklı olarak alır.byte byte olarak getırıyor yanı.image nesneside byt halındekı nesneyı anlamlı hale getırıyor.
          Image orjinalresim = Image.FromStream(fileResim.InputStream);
          Bitmap Kucukresim = new Bitmap(orjinalresim, 120, 90);//kucuk resim 150 ye 150 olsun.Bunlara bir dosya ısmı belirlemek lazım.isimlenidirmeyı farklılastıracaz.ek olarak ısmın yanına ek olarak tarıh sat vb gıbı ekleme yaparız.
            //Path =>yuklenecekolan resmın orjınal ısmını alır.GetFileNameWithoutExtension=>bu metod resım uzantısını (jpg,png vb)almadan resmın adını getirir.Filename=resim adını gonderiyor.
          //Guid.NewGuid()=>benzersız karakter olsturur.bıldıgımız guıd degıl.32 karakter olusturur.herdefasında farklı olsturur.dolayısıyla aynı ısımden resım cakısma olayı olmaz.
          //GetExtension()=>resmın uzantısını getirir.
          string dosya_adi = Path.GetFileNameWithoutExtension(fileResim.FileName)+Guid.NewGuid()+Path.GetExtension(fileResim.FileName);
            //dosyamızın adı=>dosyaadı+guid+uzantı  olarak belirlenir.Bu ismi iki yere kaydedecez.Biri KucukResim klasorune diğeri BuyukResim Klasorune.
          orjinalresim.Save(Server.MapPath("~/Content/images/Resimler/BuyukResim/"+dosya_adi));
          Kucukresim.Save(Server.MapPath("~/Content/images/Resimler/KucukResim/" + dosya_adi));
          //MapPath metodu =>dosyayı atıcagımız klosre yol belırler.
          return dosya_adi;//dosya yolu yok.dosya yolu asagıdakı haberekle action da belirticez.Buyol Serverdan klasore eklenıyor.
      }
        string VideoKaydet(HttpPostedFileBase fileVideo)
        {
            //video dda buyuk kucuk olayı yok.Geldiği gibi kaydetsek yeterlıdır bızım ıcın.
            string dosya_adi = Path.GetFileNameWithoutExtension(fileVideo.FileName) + Guid.NewGuid() + Path.GetExtension(fileVideo.FileName);
            FileStream fs = new FileStream(Server.MapPath("~/Content/Videolar/" + dosya_adi), FileMode.Create);//FileMode.Create)=>dosya modu olustur olacak.klasoru kendısı olsuturacak.
            //dosya_adi adı uzunlugu kadar byte dızısı olusturacaz.
            byte[] bufferdizi=new byte[fileVideo.ContentLength];
            fileVideo.InputStream.Read(bufferdizi,0,fileVideo.ContentLength);//buffer dızımızı uzerınde 0 dan basla dosya adı uzunlugu kadar inputstream ile oku.Sonra  filestream ile bunu yazdıracaz.
            fs.Write(bufferdizi,0,fileVideo.ContentLength);
            fs.Close();//dosyayı kullanmda kapatır.kullanma acık hale getırır.
            return dosya_adi;

        }
        //Makale ekleme
        //icine haber tipinde haber alır,hhtppostedfile tipinde resim  ve Video alır.
        [HttpPost,ValidateInput(false)]
        public ActionResult HaberEkle(Haber haber, HttpPostedFileBase Resim, HttpPostedFileBase Video,int KATEGORIID,int TIPID,Guid yazarid)
        {
            if (/*ModelState.IsValid*/haber != null)//gercekten bır model gonderılmısmı-varmı oyle bır model yanı yukardan hhtpostla gelen model varmı--varsa ıcındekıler uygulanır.
            {
                haber.H_YayimTarihi = DateTime.Now;

                //yazarıd tip guid old ıcın guıde cevırdık.
                //name sine gore gıdıp identity bulacaz.o da membership sınıfında get user a name yı veriyoruz.nameyı verırsek bıze user i (userıd getirir.)bizde user.identity.name ile kullanıcıyı verıyoruz.id dediğimiz alanı userprovidekey getirir.
                if (ModelState.IsValid)
                {


                    //haber.H_YazarID = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;
                    //haber.H_YazarID = Guid.NewGuid();
                  
                    haber.H_Goruntuleme = 0;
                    haber.H_YayimTarihi = DateTime.Now;
                    haber.H_KategoriID = KATEGORIID;
                    haber.H_TipID = TIPID;
                    haber.H_YazarID = yazarid;

                    //posted file dan gelen resimleri kaydetmemiz lazım.ondan sonra bır ısım vermemız lazım.Yanı gelen resmı once kaydedıcez.Sonra yolunu yanı adını belırlıcez.yukarıya resımlerı kaydeden bır metod yazıyoruz.
                    string dosyaadi = ResimKaydet(Resim);//resimi kaydettiririm.
                    haber.H_KucukResimYol = "/Content/images/Resimler/KucukResim/" + dosyaadi;
                    haber.H_ResimYol = "/Content/images/Resimler/BuyukResim/" + dosyaadi;
                    //Bu yol veritabanına eklenıyor.
                    //Sonra video icin yukarda bir metod tanımlıyoruz.
                    if (Video != null && Video.ContentLength > 0)//video isim uzlugu 0dan buyukse..yanı video gondermısse ıcındekı ıslem yapar.vıdeo eklememısse buraya gırmez.
                    {
                        haber.H_VideoYol = VideoKaydet(Video);
                    }
                    DB.Haber.Add(haber); //haber nesnesini veritabandaki haber tablosuna ekle.
                    DB.SaveChanges(); //veritabanına kaydet.
                }
            }
            return RedirectToAction("HaberYonet");
        }
        public ActionResult HaberListele()
        {
            return View(DB.Haber);
        }
        public ActionResult HaberSil (int id)
        {
            DB.H_Yorum.RemoveRange(DB.H_Yorum.Where(x=>x.H_Y_HaberID==id));
            DB.Haber.Remove(DB.Haber.FirstOrDefault(x=>x.H_id==id));
            DB.SaveChanges();
            return RedirectToAction("HaberYonet");
        }
        public ActionResult HaberDuzenle(int id)
        {
            Haber secili = DB.Haber.FirstOrDefault(x=>x.H_id==id);
            ViewBag.KategoriId = new SelectList(DB.H_Kategori, "H_K_ID", "H_K_Adi",secili.H_KategoriID);
            ViewBag.TipID = new SelectList(DB.H_Tip, "H_T_ID", "H_T_Adi",secili.H_TipID);
            ViewBag.YazarID = new SelectList(DB.H_Yazar, "H_Y_ID", "H_Y_Adi",secili.H_YazarID);
            return View(secili);
        }
    [HttpPost]
        public ActionResult HaberDuzenle(Haber hbr ,HttpPostedFileBase Foto)
        {
            var hbrdznle = DB.Haber.FirstOrDefault(x=>x.H_id==hbr.H_id);
            hbrdznle.H_Baslik = hbr.H_Baslik;
            hbrdznle.H_ozet = hbr.H_ozet;
            hbrdznle.H_icerik = hbr.H_icerik;
            hbrdznle.H_KategoriID = hbr.H_KategoriID;
            hbrdznle.H_YazarID = hbr.H_YazarID;
            hbrdznle.H_TipID = hbr.H_TipID;
            hbrdznle.H_VideoYol = hbr.H_VideoYol;
        if(Foto!=null&&Foto.ContentLength>0)
        {
            string name=ResimKaydet(Foto);
            hbr.H_ResimYol = "/Content/images/Resimler/BuyukResim/" + name;
            hbr.H_KucukResimYol = "/Content/images/Resimler/KucukResim/" + name;
        }
        DB.SaveChanges();
            return RedirectToAction("HaberYonet");
        }
    }
}