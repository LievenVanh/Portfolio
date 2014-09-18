using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrolIndexer.Models;
using BrolIndexer.Services;

namespace BrolIndexer.Controllers
{
    public class HerstellingController : Controller
    {
        private HerstellingenEntities db = new HerstellingenEntities();
        private HerstellingService service = new HerstellingService();

        //
        // GET: /Herstelling/
        [Authorize]
        public ActionResult Index()
        {
            if (User.IsInRole("Technicus") || User.IsInRole("Admin"))
            {
                return View(service.GetAlleHerstellingen());
            }
            else
            {
                return View(service.GetHerstellingenKlant(User.Identity.Name));
            }
        }

        //
        // GET: /Herstelling/Details/5
        [Authorize]
        public ActionResult Details(int id = 0)
        {
            Herstelbon herstelbon = service.GetHerstelbon(id);
            if (herstelbon == null)
            {
                ViewBag.ErrorMessage = "Herstelbon met ID " + id + " bestaat niet.";
                ViewBag.Url = Url.Action("Index", "Herstelling");
                return View("Error");
            }
            return View(herstelbon);
        }

        //
        // GET: /Herstelling/Create
        [Authorize(Roles = "admin, technicus")]
        public ActionResult Create()
        {
            ViewBag.KlantId = new SelectList(db.Klanten, "KlantId", "Naam");
            ViewBag.TechnicusId = new SelectList(db.Technici, "TechnicusId", "Naam");
            ViewBag.StatusId = new SelectList(db.Statussen, "StatusId", "StatusNaam");
            return View();
        }

        //
        // POST: /Herstelling/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,technicus")]
        public ActionResult Create(Herstelbon herstelbon)
        {
            
            if (ModelState.IsValid)
            {
                herstelbon.Aanmaakdatum = DateTime.Now;     //Nieuwe herstelbon datum kan niet aangepast worden om fouten tegen te gaan.
                db.Herstelbonnen.Add(herstelbon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KlantId = new SelectList(db.Klanten, "KlantId", "Naam", herstelbon.KlantId);
            ViewBag.TechnicusId = new SelectList(db.Technici, "TechnicusId", "Naam", herstelbon.TechnicusId);
            ViewBag.StatusId = new SelectList(db.Statussen, "StatusId", "StatusNaam", herstelbon.StatusId);
            return View(herstelbon);
        }

        //
        // GET: /Herstelling/Edit/5
        [Authorize(Roles = "admin,technicus")]
        public ActionResult Edit(int id = 0)
        {
            Herstelbon herstelbon = db.Herstelbonnen.Find(id);
            if (herstelbon == null)
            {
                ViewBag.ErrorMessage = "Herstelbon met ID " + id + " bestaat niet.";
                ViewBag.Url = Url.Action("Index", "Herstelling");
            }
            ViewBag.KlantId = new SelectList(db.Klanten, "KlantId", "Naam", herstelbon.KlantId);
            ViewBag.TechnicusId = new SelectList(db.Technici, "TechnicusId", "Naam", herstelbon.TechnicusId);
            ViewBag.StatusId = new SelectList(db.Statussen, "StatusId", "StatusNaam", herstelbon.StatusId);
            return View(herstelbon);
        }

        //
        // POST: /Herstelling/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,technicus")]
        public ActionResult Edit(Herstelbon herstelbon)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(herstelbon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KlantId = new SelectList(db.Klanten, "KlantId", "Naam", herstelbon.KlantId);
            ViewBag.TechnicusId = new SelectList(db.Technici, "TechnicusId", "Naam", herstelbon.TechnicusId);
            ViewBag.StatusId = new SelectList(db.Statussen, "StatusId", "StatusNaam", herstelbon.StatusId);
            return View(herstelbon);
        }

        //
        // GET: /Herstelling/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id = 0)
        {
            Herstelbon herstelbon = db.Herstelbonnen.Find(id);
            if (herstelbon == null)
            {
                ViewBag.ErrorMessage = "Herstelbon met ID " + id + " bestaat niet.";
                ViewBag.Url = Url.Action("Index", "Herstelling");
            }
            return View(herstelbon);
        }

        //
        // POST: /Herstelling/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Herstelbon herstelbon = db.Herstelbonnen.Find(id);
            db.Herstelbonnen.Remove(herstelbon);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        [Authorize(Roles = "Technicus,Admin")]
        public ActionResult AlleHerstellingenVanKlant(int? id)
        {
            var db = new HerstellingService();
            return View("Index",db.GetHerstellingenKlant(id));
        }

        [Authorize(Roles = "Technicus,Admin")]
        public ActionResult AlleHerstellingenVanTechnicus(int id)
        {
            var db = new HerstellingService();
            return View("Index",db.GetHerstellingenTechnicus(id));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            service.Dispose();
            base.Dispose(disposing);
        }
    }
}