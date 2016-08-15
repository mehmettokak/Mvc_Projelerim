using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HocaWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(name: "Admin", url: "Admin", defaults: new { controller = "Admin", action = "giris" });
            routes.MapRoute(name: "GirisKontrol", url: "Admin/Login",defaults: new { controller = "Admin", action = "GirisKontrol" });
            routes.MapRoute(name: "AdminPage", url: "Admin/Page",defaults: new { controller = "Admin", action = "AdminPage" });

            routes.MapRoute(name: "Logout", url: "Admin/Logout", defaults: new { controller = "Admin", action = "Logout" });

            //-------------------------------------------ALTKATEGORİ--------------------------------
            routes.MapRoute(name: "altkategoriSayfa", url: "Admin/Menu-Sub-Category", defaults: new { controller = "Admin", action = "altkategori" });
            routes.MapRoute(name: "altkategoriekle", url: "Admin/Add-Sub-Category", defaults: new { controller = "Admin", action = "AltKategoriEkle" });
            routes.MapRoute(name: "altkategoriSil", url: "Admin/Delete-Sub-Category", defaults: new { controller = "Admin", action = "AltKategoriSil" });
            routes.MapRoute(name: "altkategoriGuncelle", url: "Admin/Update-Sub-Category", defaults: new { controller = "Admin", action = "AltKategoriGuncelle" });
            //----------------------------------------KATEGORİ-------------------------------------------

            routes.MapRoute(name: "kategoriSayfa", url: "Admin/Category", defaults: new { controller = "Admin", action = "kategori" });

            routes.MapRoute(name: "kategoriekle", url: "Admin/Add-Category", defaults: new { controller = "Admin", action = "KategoriEkle" });
            routes.MapRoute(name: "kategoriGuncelle", url: "Admin/Update-Category", defaults: new { controller = "Admin", action = "KategoriGuncelle" });
            routes.MapRoute(name: "kategoriSil", url: "Admin/Delete-Category", defaults: new { controller = "Admin", action = "KategoriSil" });

            //-----------------------------------------URUNLER-------------------------------------------------------------
            routes.MapRoute(name: "urunlerSayfa", url: "Admin/Product", defaults: new { controller = "Admin", action = "urunler" });
            routes.MapRoute(name: "urunlerEkle", url: "Admin/Add-Update-Product", defaults: new { controller = "Admin", action = "UrunEkleGuncelle" });
            routes.MapRoute(name: "urunlerSil", url: "Admin/Delete-Product", defaults: new { controller = "Admin", action = "UrunSil" });

            //--------------------------------Home-contact Guncelle-------------------------------------------------------------
            routes.MapRoute(name: "HomeGuncelle", url: "HomeUpdate", defaults: new { controller = "Sabitler", action = "HomeGuncelle" });
            routes.MapRoute(name: "ContactSayfa", url: "ContactUpdate", defaults: new { controller = "Sabitler", action = "Contact" });
            routes.MapRoute(name: "HomeSayfa", url: "Home-Update", defaults: new { controller = "Sabitler", action = "Home" });
            routes.MapRoute(name: "HomeImageUpdate", url: "Home-Imge-Update", defaults: new { controller = "Sabitler", action = "H_Img_update" });

            routes.MapRoute(name: "UserUpdate", url: "UserUpdate", defaults: new { controller = "Sabitler", action = "UserPaswordGuncelle" });
            routes.MapRoute(name: "SendEmail", url: "Send-Email", defaults: new { controller = "Sayfa", action = "Send_Email" });



            routes.MapRoute(name: "ScientificWork", url: "ScientificWork/{katg}/{sayfa}", defaults: new { controller = "Scientific", action = "Work", sayfa = 1, kategori = "hepsi" });
            routes.MapRoute(name: "FieldWorks", url: "FieldWorks/{katg}/{sayfa}",defaults: new { controller = "Field", action = "Works", sayfa = 1, kategori = "hepsi" });
            routes.MapRoute(name: "Miscellaneous", url: "Mscllns/{katg}/{sayfa}",defaults: new { controller = "Miscellaneous", action = "Miscellaneous", sayfa = 1, kategori = "hepsi" });


            routes.MapRoute(name: "Index", url: "{controller}/{action}/{id}",defaults: new { controller = "Sayfa", action = "Index", id = UrlParameter.Optional });
           
            routes.MapRoute(name: "Contacs", url: "{controller}/{action}/{id}",defaults: new { controller = "Sayfa", action = "Contact", id = UrlParameter.Optional });
        }
    }
}
