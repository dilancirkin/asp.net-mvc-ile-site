using BlogSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Controllers
{
    [Authorize]
    public class YaziController : Controller
    {
        // GET: Yazi
        BlogSiteEntities1 context = new BlogSiteEntities1();
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Detay(int id)
        {
            var data = context.Yazi.FirstOrDefault(x => x.Yaziid == id);
            return View(data);

        }

  
    }
}