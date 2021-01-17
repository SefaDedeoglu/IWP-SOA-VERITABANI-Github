using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IWP_SOA_VERITABANI.SqlModel;

namespace IWP_SOA_VERITABANI.Controllers
{
    public class SorularsController : Controller
    {
        private siteDbEFW db = new siteDbEFW();

        // GET: Sorulars
        public ActionResult Index()
        {
            var sorulars = db.Sorulars.Include(s => s.Kategori);
            return View(sorulars.ToList());
        }

        // GET: Sorulars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sorular sorular = db.Sorulars.Find(id);
            if (sorular == null)
            {
                return HttpNotFound();
            }
            return View(sorular);
        }

        // GET: Sorulars/Create
        public ActionResult Create()
        {
            ViewBag.kategoriID = new SelectList(db.Kategoris, "kategoriID", "kategoriAd");
            return View();
        }

        // POST: Sorulars/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "soruID,soruBaslik,soruOzet,soruIcerik,soruResim,soruTarih,soruGorunme,soruCevapSayisi,kategoriID")] Sorular sorular)
        {
            if (ModelState.IsValid)
            {
                db.Sorulars.Add(sorular);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.kategoriID = new SelectList(db.Kategoris, "kategoriID", "kategoriAd", sorular.kategoriID);
            return View(sorular);
        }

        // GET: Sorulars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return Redirect("/");
            }
            Sorular sorular = db.Sorulars.Find(id);
            if (sorular == null)
            {
                return HttpNotFound();
            }
            ViewBag.kategoriID = new SelectList(db.Kategoris, "kategoriID", "kategoriAd", sorular.kategoriID);
            return View(sorular);
        }

        // POST: Sorulars/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "soruID,soruBaslik,soruOzet,soruIcerik,soruResim,soruTarih,soruGorunme,soruCevapSayisi,kategoriID")] Sorular sorular)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sorular).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.kategoriID = new SelectList(db.Kategoris, "kategoriID", "kategoriAd", sorular.kategoriID);
            return View(sorular);
        }

        // GET: Sorulars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sorular sorular = db.Sorulars.Find(id);
            if (sorular == null)
            {
                return HttpNotFound();
            }
            return View(sorular);
        }

        // POST: Sorulars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sorular sorular = db.Sorulars.Find(id);
            db.Sorulars.Remove(sorular);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
