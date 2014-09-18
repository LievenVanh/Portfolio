using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrolIndexer.Models;
using BrolIndexer.Services;

namespace BrolIndexer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Registreer of log in om de verschillende mogelijkheden te zien";
            return View();
        }
        

        public ActionResult About()
        {
            ViewBag.Message = "Je omschrijvingspagina.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Je contactenpagina.";

            return View();
        }
       


    }
}
