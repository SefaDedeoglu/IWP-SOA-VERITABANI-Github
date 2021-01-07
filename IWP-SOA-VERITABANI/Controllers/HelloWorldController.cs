using IWP_SOA_VERITABANI.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IWP_SOA_VERITABANI.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: HelloWorld
        public ActionResult Index()
        {
            return View();
        }

        public string welcome(string name)
        {
            return System.Web.HttpUtility.HtmlEncode($"Hello {name}");
        }

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Delete()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Details()
        {
            return View();
        }
    }
}