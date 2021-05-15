using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using BlogSite.Models;
namespace BlogSite.Areas.admin.Controllers
{
    [Authorize]
    public class HakkimizdaController : Controller
    {
        // GET: admin/Hakkimizda
        public ActionResult Index()
        {
            using (BlogSiteEntities1 db = new BlogSiteEntities1())
            {
                var model = db.Hakkimizda.First();
                return View(model);
            }
                
        }
        public ActionResult HakkimizdaGuncelle()
        {
            using (BlogSiteEntities1 db = new BlogSiteEntities1())
            {
                var model = db.Hakkimizda.First();
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Kaydet(Hakkimizda GelenVeri)
        {
            using (BlogSiteEntities1 db =new BlogSiteEntities1())
            {
                var GuncellenecekVeri = db.Hakkimizda.First();
                if(!ModelState.IsValid)
                {
                    return View("HakkimizdaGuncelle",GelenVeri);
                }
                if (GelenVeri.resimfile != null)
                {
                    GelenVeri.Resim = GelenVeri.resimfile.FileName;
                    GelenVeri.resimfile.SaveAs(Path.Combine(Server.MapPath("~/Content/Logo/"), Path.GetFileName(GelenVeri.Resim)));


                }
                db.Entry(GuncellenecekVeri).CurrentValues.SetValues(GelenVeri);
                db.SaveChanges();
                return RedirectToAction("index", "hakkimizda");
                
            }
        }
    }
    
}