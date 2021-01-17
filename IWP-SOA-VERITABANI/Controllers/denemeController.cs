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
using System.Web.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace IWP_SOA_VERITABANI.Controllers
{
    public class denemeController : Controller
    {
        private siteDBEntities1 db = new siteDBEntities1();
        // GET: deneme

     

        public ActionResult Index(int? id)
        {
            Session["HATA"] = null;
            if (id == null)
            {
                var sorular = db.Sorulars;
                Kullanici kullanici = null;
                return View(Tuple.Create<Kullanici, IEnumerable<Sorular>>(kullanici, sorular.ToList()));
            }
            else
            {

                
                
                var sorular = db.Sorulars;
                Kullanici kullanici = db.Kullanicis.Find(id);
               
                if (kullanici == null)
                {
                    return Redirect("/deneme/deneme/index");
                }
                else
                return View(Tuple.Create<Kullanici, IEnumerable<Sorular>>(kullanici, sorular.ToList()));
            }

        }
 

        public ActionResult login()
        {
            if (Session["HATA"] != null)
            {
                ViewBag.HATA = "Kullanıcı adı veya şifre hatalı";
            }
            return View();
        }
        [HttpPost]
        public ActionResult Logincontrol(FormCollection form)
        {
            
            string username = form["username"].Trim();
            string pass = form["password"].Trim();
            var deneme = db.Kullanicis.FirstOrDefault(x => x.KullaniciAd == username && x.KullaniciPW == pass);
            if(deneme !=null)
            {
                Session["HATA"] = null;
                Session["KullanıcıAdı"] = deneme.KullaniciAd;
                return Redirect("/deneme/deneme/index");
            }
            else
            {
                Session["HATA"] = username;
                return Redirect("/deneme/deneme/login");
            }
           
            

        }

        [HttpGet]
        public ActionResult Add()
        {
            return Redirect("/deneme/deneme/index");
        }





        [HttpPost]
        public ActionResult Add(FormCollection form)
        {
            if (Request.HttpMethod == "GET")
            {
                return Redirect("/deneme/deneme/Index");
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

        public ActionResult Iletisim(FormCollection form)
        {
            iletisim iletisim = new iletisim();
            iletisim.iletisimMail = form["email"].Trim();
            iletisim.iletisimIcerik = form["message"].Trim();
            iletisim.iletisimAdSoyad = form["name"].Trim();
            iletisim.iletisimTarih = DateTime.Now;
            db.iletisims.Add(iletisim);
            db.SaveChanges();

            return View();


        }

        public ActionResult Sorugonder(FormCollection form)
        {
            Cevaplar cevap = new Cevaplar();
            cevap.soruID = Convert.ToInt32(form["id"].Trim());
            cevap.cevapTarih = DateTime.Now;
            cevap.cevapOnay = false;

            if (!Request.Files[0].FileName.Equals(""))
            {
                Random rnd = new Random();
                string FilePath = Path.GetFileName(Request.Files[0].FileName);
                string filext = Path.GetExtension(Request.Files[0].FileName);
                string randomyer = rnd.Next(12545).ToString();
                string fPath = randomyer + FilePath;

                if (filext.Equals(".svg") || filext.Equals(".png") || filext.Equals(".jpg") || filext.Equals(".jpeg") || filext.Equals(".gif") || filext.Equals("PNG"))
                {
                    Request.Files[0].SaveAs("C:/Users/Sefa/Desktop/Proje-Ocak/IWP-SOA-VERITABANI-Github/IWP-SOA-VERITABANI/images/" + randomyer + FilePath);
                    cevap.cevapResim = fPath;
                }
            }
            Sorular soru = db.Sorulars.Find(cevap.soruID);
            soru.soruCevapSayisi += 1;
            
            cevap.cevapIcerik = form["textInput"].Trim();
            if (Session["KullanıcıAdı"] != null)
            {
                string name = Session["KullanıcıAdı"].ToString();
                cevap.cevapAdSoyad = name;
            }
            db.Cevaplars.Add(cevap);
            db.SaveChangesAsync();




            return View(cevap);
        }
        public ActionResult quit()
        {
            Session["KullanıcıAdı"] = null;
            return Redirect("/deneme/deneme/index");
        }

        public ActionResult kaydol()
        {
            return View();
        }
        [HttpPost]
        public ActionResult kayıt(FormCollection form)
        {
            Kullanici kullanici = new Kullanici();
            kullanici.KullaniciAd = form["txtfname"].Trim();
            kullanici.KullaniciPW = form["txtpass"].Trim();
            kullanici.KullaniciMail = form["txtmail"].Trim();
            db.Kullanicis.Add(kullanici);
            db.SaveChanges();
            return View();
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
                return Redirect("/deneme/deneme/Index");
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