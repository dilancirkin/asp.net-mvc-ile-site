using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogSite.App_Classes;
using BlogSite.Models;

namespace BlogSite.Areas.admin.Controllers
{
    [Authorize]
    public class YazilarController : Controller
    {
        // GET: admin/Yazilar
        public ActionResult Index()
        {
            using (BlogSiteEntities1 db=new BlogSiteEntities1())
            {
                var model = db.Yazi.ToList();
                return View(model);
            }
        }
        public ActionResult YaziEkle()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult YaziEkle(Yazi yazi,HttpPostedFileBase resim)
        {
            using (BlogSiteEntities1 db = new BlogSiteEntities1())
            {
                Image img = Image.FromStream(resim.InputStream);
                Bitmap kckResim = new Bitmap(img, Settings.ResimKucukBoyut);
                Bitmap ortaResim = new Bitmap(img, Settings.ResimOrtaBoyut);
                Bitmap bykResim = new Bitmap(img, Settings.ResimBuyukBoyut);

                kckResim.Save(Server.MapPath("/Content/YaziResim/KucukBoyut/" + resim.FileName));
                ortaResim.Save(Server.MapPath("/Content/YaziResim/OrtaBoyut/" + resim.FileName));
                bykResim.Save(Server.MapPath("/Content/YaziResim/BuyukBoyut/" + resim.FileName));

                Resim rsm = new Resim();
                rsm.BuyukBoyut = "/Content/YaziResim/BuyukBoyut/" + resim.FileName;
                rsm.OrtaBoyut = "/Content/YaziResim/OrtaBoyut/" + resim.FileName;
                rsm.KucukBoyut = "/Content/YaziResim/KucukBoyut/" + resim.FileName;

                db.Resim.Add(rsm);
                db.SaveChanges();
                yazi.ResimID = rsm.ResimID;
                yazi.EklenmeTarihi = DateTime.Now;
                int yzrId = db.Yazar.FirstOrDefault(x => x.KullaniciAdi == User.Identity.Name).YazarID;
                yazi.YazarID = yzrId;
                db.Yazi.Add(yazi);
                db.Configuration.LazyLoadingEnabled = false;
                db.SaveChanges();


                return RedirectToAction("Index");


            }
           

        }
        public ActionResult Guncelle(int Yaziid)
        {
            using (BlogSiteEntities1 db = new BlogSiteEntities1())
            {
                var model = db.Yazi.Find(Yaziid);
                if(model==null)
                {
                    return HttpNotFound();
                }
                return View("Guncelle",model);
            }
        }
       
        [HttpPost]
        public ActionResult Kaydet(Yazi GelenVeri)
        {
            using (BlogSiteEntities1 db = new BlogSiteEntities1())
            {
                var GuncellenecekVeri = db.Yazi.ToList();
                if (!ModelState.IsValid)
                {
                    return View("YazarGuncelle", GelenVeri);
                }
                
                if (!ModelState.IsValid)
                {
                    return View("Guncelle", GelenVeri);
                }
                db.Entry(GuncellenecekVeri).CurrentValues.SetValues(GelenVeri);
                db.SaveChanges();
                return RedirectToAction("index", "yazilar");
            }
        }


    }
}