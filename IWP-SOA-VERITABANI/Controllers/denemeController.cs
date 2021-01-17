using IWP_SOA_VERITABANI.SqlModel;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using IWP_SOA_VERITABANI.SqlModel;

namespace IWP_SOA_VERITABANI.Controllers
{
    public class denemeController : Controller
    {
        private siteDbEFW db = new siteDbEFW();
        // GET: deneme
        
        public ActionResult Index()
        {
            ViewBag.kategoriID = new SelectList(db.Kategoris, "kategoriID", "kategoriAd");

            var sorulars = db.Sorulars;
            return View(sorulars.ToList());
        }
    
        public ActionResult login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            return Redirect("/deneme/index");
        }





        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            if (Request.HttpMethod == "GET")
            {
                return Redirect("/deneme/Index");
            }
            else
            {
                
                string a;
                Sorular soru = new Sorular();
                 a = form["bolum"].Trim();
                soru.soruIcerik = form["textInput"].Trim();
                soru.soruBaslik = a + " Matematik";
                soru.soruCevapSayisi = 0;
                soru.soruGorunme = 0;
                soru.soruOzet = soru.soruIcerik.Substring(0, soru.soruIcerik.Length/3)+"   ... ";
                if (a.Equals(db.Kategoris.Find(2).kategoriAd.ToString()))
                {
                    soru.kategoriID = db.Kategoris.Find(2).kategoriID;
                }
                else
                {
                    soru.kategoriID = db.Kategoris.Find(1).kategoriID;

                }
                

                if (!Request.Files[0].FileName.Equals(""))
                {
                    Random rnd = new Random();
                    string FilePath = Path.GetFileName(Request.Files[0].FileName);
                    string filext = Path.GetExtension(Request.Files[0].FileName);
                    string randomyer = rnd.Next(12545).ToString();
                    string fPath =  randomyer  + FilePath;

                    if (filext.Equals(".svg") || filext.Equals(".png") || filext.Equals(".jpg") || filext.Equals(".jpeg") || filext.Equals(".gif")||filext.Equals("PNG"))
                    {
                        Request.Files[0].SaveAs("C:/Users/Sefa/Desktop/Proje-Ocak/IWP-SOA-VERITABANI-Github/IWP-SOA-VERITABANI/images/" + randomyer + FilePath);
                        soru.soruResim = fPath;
                    }

                        }
                soru.soruTarih = DateTime.Now;
                db.Sorulars.Add(soru);
                db.SaveChanges();
                

           
                return View();
            }
            

        }
        [HttpGet]
        public ActionResult sorular()
        {
            var sorulars = db.Sorulars;
            return View(sorulars.ToList());
        }
        [HttpGet]
        public ActionResult Aytsorular()
        {
            var sorulars = db.Sorulars;
            return View(sorulars.ToList());
        }

        public ActionResult soru(int id)
        {
            if (id == null)
            {
                return Redirect("/deneme/Index");
            }

            Sorular sorular = db.Sorulars.Find(id);
            var cevaplar = db.Cevaplars;
            if (sorular == null)
            {
                return HttpNotFound();
            }
            
            return View(Tuple.Create<Sorular, IEnumerable<Cevaplar> >(sorular,cevaplar.ToList()));
        }
    }
}