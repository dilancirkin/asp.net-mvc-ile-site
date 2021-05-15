using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Controllers
{
    using Models;
    public class KategoriController : Controller
    {
        // GET: Kategori
        BlogSiteEntities1 context = new BlogSiteEntities1();
        public ActionResult Index(int id)
        {
            return View(id);
        }
        public PartialViewResult KategoriWidget()
        {
            return PartialView(context.Kategori.ToList());
        }
        public ActionResult YaziListele(int id)
        {
            var data = context.Yazi.Where(x => x.KategoriID == id).ToList();
            return View("YaziListeleWidget", data);
        }
    }
}