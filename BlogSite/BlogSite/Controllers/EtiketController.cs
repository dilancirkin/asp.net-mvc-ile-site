using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Controllers
{
    using Models;
    public class EtiketController : Controller
    {
        // GET: Etiket
        BlogSiteEntities1 context = new BlogSiteEntities1();
        public ActionResult Index(int id)
        {
            return View(id);
        }
        public PartialViewResult EtiketlerWidget()
        {
            return PartialView(context.Etiket.ToList());
        }
        public ActionResult YaziListele(int id)
        {
            var data = context.Yazi.Where(x => x.Etiket.Any(y=>y.EtiketID==id)).ToList();
            return View("YaziListeleWidget", data);
        }
    }
}