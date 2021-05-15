using BlogSite.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Areas.admin.Controllers
{
    [Authorize]
    public class YazarlarController : Controller
    {
        // GET: admin/Yazarlar
        public ActionResult Index()
        {
            using (BlogSiteEntities1 db = new BlogSiteEntities1())
            {
                var model = db.Yazar.ToList();

                return View(model);
            }
        }
        public ActionResult YazarGuncelle(int? YazarID)
        {
            using (BlogSiteEntities1 db = new BlogSiteEntities1())
            {
                Yazar model = db.Yazar.Find(YazarID);
                if (YazarID==null|| YazarID==0)
                {
                    return HttpNotFound();
                }
               
                return View("YazarGuncelle",model);
            }
        }
        [HttpPost]
        public ActionResult Kaydet(Yazar GelenVeri)
        {
            using (BlogSiteEntities1 db = new BlogSiteEntities1())
            {
                var GuncellenecekVeri = db.Yazar.ToList();
                if (!ModelState.IsValid)
                {
                    return View("YazarGuncelle", GelenVeri);
                }
                if (GelenVeri.resimfile != null)
                {
                    GelenVeri.Resim = GelenVeri.resimfile.FileName;
                    GelenVeri.resimfile.SaveAs(Path.Combine(Server.MapPath("~/Content/YazarResim/"), Path.GetFileName(GelenVeri.Resim)));


                }
                if (!ModelState.IsValid)
                {
                    return View("YazarGuncelle", GelenVeri);
                }
                db.Entry(GuncellenecekVeri).CurrentValues.SetValues(GelenVeri);
                db.SaveChanges();
                return RedirectToAction("index", "yazarlar");
            }
        }
    }
}