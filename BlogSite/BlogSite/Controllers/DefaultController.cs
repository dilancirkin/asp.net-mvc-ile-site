using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogSite.Models;

namespace BlogSite.Controllers
{
    
    using App_Classes;
    public class DefaultController : Controller
    {
        // GET: Default
        BlogSiteEntities1 context = new BlogSiteEntities1();
        public ActionResult Index()
        {
            return View();
        }
        [Route("Hakkimizda")]
        public ActionResult Hakkimizda()
        {
            using (BlogSiteEntities1 db = new BlogSiteEntities1())
            {
                var model = db.Hakkimizda.Find(1);
                return View(model);
            }
                
        }
        [Route("Yazarlar")]
          public ActionResult Yazarlar()
        {
            using (BlogSiteEntities1 db = new BlogSiteEntities1())
            {
                var model = db.Yazar.ToList();
                return View(model);
            }

        }

        [Route("Yazilar")]
        public ActionResult Yazilar()
        {
            using (BlogSiteEntities1 db = new BlogSiteEntities1())
            {
                var model = db.Yazi.ToList();
                return View(model);
            }

        }
        [Route("Yazi")]
        public ActionResult Detay(int id)
        {
            using (BlogSiteEntities1 db= new BlogSiteEntities1())
            {
                var data = context.Yazi.FirstOrDefault(x => x.Yaziid == id);
                if (data==null)
                {
                    return HttpNotFound();
                }
                return View(data);
            }
        }
        [Route("Iletisim")]
        public ActionResult Iletisim()
        {

            using (BlogSiteEntities1 db = new BlogSiteEntities1())
            {
                var model = db.Yazar.ToList();
                return View(model);
            }
        }


        public ActionResult YaziListele()
        {
            var data = context.Yazi.ToList();
            return View("YaziListeleWidget", data);
        }
        public PartialViewResult PopulerMakalelerWidget()
        {
            var model = context.Yazi.OrderByDescending(x => x.EklenmeTarihi).Take(3).ToList();
            return PartialView(model);
        }

        public ActionResult YazilarListele()
        {
            var model= context.Yazi.ToList();
            return View("YazilarListeleWidget", model);
        }

    }
}