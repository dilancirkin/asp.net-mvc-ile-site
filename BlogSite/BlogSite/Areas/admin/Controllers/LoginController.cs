using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogSite.Models;
using System.Web.Security;

namespace BlogSite.Areas.admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: admin/Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alogin(Yazar yazarFormu)
        {
            using (BlogSiteEntities1 db = new BlogSiteEntities1())
            {
                var yazarVarmi = db.Yazar.FirstOrDefault(x=>x.KullaniciAdi==yazarFormu.KullaniciAdi
                && x.Parola==yazarFormu.Parola);
                if (yazarVarmi !=null)
                {
                    FormsAuthentication.SetAuthCookie(yazarVarmi.KullaniciAdi, false);
                    return  RedirectToAction("/index", "Yazilar");
                }
                ViewBag.Hata = "Kullanıcı adı veya şifre hatalı!";
                return View("index");
            }

                
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index");
        }
    }
}