using BlogSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogSite.Controllers
{
    public class KullaniciController : Controller
    {
        // GET: Kullanici
        BlogSiteEntities1 context = new BlogSiteEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GirisYap(Kullanici kl)
        {
            string rol = ValidateUser(kl.KullaniciAdi, kl.Parola);
            if (!string.IsNullOrEmpty(rol))
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, kl.KullaniciAdi, DateTime.Now, DateTime.Now.AddMinutes(15), true, rol, FormsAuthentication.FormsCookiePath);

                HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName);
                if (ticket.IsPersistent)
                {
                    ck.Expires = ticket.Expiration;
                }
                Response.Cookies.Add(ck);
                Session["rol"] = rol;
                Response.Redirect(FormsAuthentication.GetRedirectUrl(kl.KullaniciAdi, true));
                return RedirectToAction("Index", "Default");
            }

            return RedirectToAction("GirisYap");
        }

        string ValidateUser(string ka, string pwd)
        {
            Kullanici kl = context.Kullanici.FirstOrDefault(x => x.KullaniciAdi == ka && x.Parola == pwd);
            if (kl != null)
            {
                return kl.Rol.RolAdi;
            }

            ViewBag.Hata = "Kullanıcı adı veya şifre hatalı!";
            return "";
        }
    }
}