using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HocaWeb.Models;

namespace HocaWeb.Controllers
{
    public class SayfaController : Controller
    {
        // GET: Sayfa
        public ActionResult Index(int? sayfa,string katg)
        {

            Field wrk = new Field();
            wrk = Field.ResimGetir(sayfa, katg);
            return View("Index", wrk);
        }


      
        public ActionResult Contact(int? sayfa,string katg)
        {
            Field wrk = new Field();
            wrk = Field.ResimGetir(sayfa, katg);
            return View("Contact", wrk);
           
        }

        [HttpPost]
        public ActionResult Send_Email(int? id ,string fulname, string email, string desc, int? sayfa, string katg,string konu,bool durum)
        {
            using (var _db = new Models.AkademikEntities())
            {
                try
                {
                    _db.sp_mehmet_iletisim_ekle(id,fulname, email, desc, konu,durum);
                    ViewBag.Error1 = "<span style='color: green; float:left;'>Your Message Has Been Successfully Sent To</span>";
                   
                }
                catch (Exception)
                {
                    ViewBag.Error1 = "<span style='color: red; float:left; '>Error!! Please Try Again</span>";
                }

            }
            Field wrk = new Field();
            wrk = Field.ResimGetir(sayfa, katg);
           return View("Contact",wrk); 

        }
    }
    
}