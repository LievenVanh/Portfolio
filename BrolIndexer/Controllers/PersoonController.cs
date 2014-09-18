using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using BrolIndexer.Models;
using BrolIndexer.Services;

namespace BrolIndexer.Controllers
{
    public class PersoonController : Controller
    {
        HerstellingenEntities db = new HerstellingenEntities();
        HerstellingService service = new HerstellingService();
        
        [HttpGet]
        [Authorize(Roles = "Technicus, admin")]
        public ActionResult Upload(int id)
        {
            return View(id);
        }

        [HttpPost]
        [Authorize(Roles = "Technicus, admin")]
        public ActionResult FotoUpload(int id)
        {
            if (Request.Files.Count > 0)
            {
                var foto = Request.Files[0];
                foto.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Content/fotos"),id +".jpg"));
            }
            return RedirectToAction("GeefTechnicusWeer", new{ID=id});
        }

        [Authorize(Roles = "Technicus,Admin")]
        public ActionResult GeefKlantWeer(int id)
        {
            return View(service.GetKlant(id));
        }


        [Authorize(Roles = "Admin,technicus")]
        public ActionResult GeefAlleKlantenWeer()
        {
            return View(service.GetAlleKlanten());
        }

        [Authorize]
        public ActionResult GeefTechnicusWeer(int id)
        {
            return View(service.GetTechnicus(id));
        }

        public ActionResult AllePersoneelWeergeven()
        {
            return View((service.GetAlleTechnici()));
        }

        [Authorize(Roles = "technicus, admin")]
        public ActionResult CreateKlant()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "technicus, admin")]
        public ActionResult CreateKlant(Klant klant)
        {
            if (ModelState.IsValid)
            {
                db.Klanten.Add(klant);
                db.SaveChanges();
                if (Session["BewerkModus"]!=null)
                    System.Web.HttpContext.Current.Session.Remove("BewerkModus");
                    return RedirectToAction("Create", "Herstelling");
                    return RedirectToAction("Index", "Home");
            }
            return View(klant);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            service.Dispose();
            base.Dispose(disposing);
        }
    }
}
