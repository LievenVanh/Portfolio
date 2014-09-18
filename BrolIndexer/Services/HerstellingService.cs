using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.WebPages;
using BrolIndexer.Models;
using Microsoft.Ajax.Utilities;

namespace BrolIndexer.Services
{
    public class HerstellingService :IDisposable
    {
        HerstellingenEntities db = new HerstellingenEntities();
        public List<Herstelbon> GetAlleHerstellingen()
        {
            return (from herstelling in db.Herstelbonnen.Include("Klant").Include("Technicus").Include("Status") select herstelling)
                        .ToList();
            
        }

        public List<Herstelbon> GetHerstellingenKlant(string login)
        {
            return (from herstelling in db.Herstelbonnen.Include("Klant").Include("Technicus").Include("Status")
                    where herstelling.Klant.Login == login
                    select herstelling).ToList();
            
        }

        public List<Herstelbon> GetHerstellingenKlant(int? id)
        {
            if (id == null) return GetAlleHerstellingen();
            return (from herstelling in db.Herstelbonnen.Include("Klant").Include("Technicus").Include("Status")
                        where herstelling.KlantId == id
                        select herstelling).ToList();
                
            
        }

        public List<Herstelbon> GetHerstellingenTechnicus(int id)
        {
            return (from herstelling in db.Herstelbonnen.Include("Klant").Include("Technicus").Include("Status")
                    where herstelling.TechnicusId == id
                    select herstelling).ToList();
            
        }

        public Herstelbon GetHerstelbon(int id)
        {
            return db.Herstelbonnen.Find(id);
        }

        

        public int GetKlantIdFromLogin(string login)
        {
            return (from klant in db.Klanten where klant.Login == login select klant.KlantId).FirstOrDefault();
            
        }

        public Klant GetKlant(int id)
        {
            return (from klant in db.Klanten where klant.KlantId == id select klant).FirstOrDefault();
            
        }

        public Technicus GetTechnicus(int id)
        {
            return (from technicus in db.Technici where technicus.TechnicusId == id select technicus).FirstOrDefault();
            
        }
        
        public List<Klant> GetAlleKlanten()
        {
            return db.Klanten.ToList();
            
        }

        public List<Technicus> GetAlleTechnici()
        {
            return db.Technici.ToList();
            
        }

        

        public void VoegHerstellingToe(Herstelbon herstelbon)
        {
            db.Herstelbonnen.Add(herstelbon);
            db.SaveChanges();
            
        }

        public void VoegNieuweKlantToe(Klant klant)
        {
            db.Klanten.Add(klant);
            db.SaveChanges();
        }
        
        public void VoegNieuweTechnicusToe(Technicus technicus)
        {
            db.Technici.Add(technicus);
            db.SaveChanges();
            
        }

        

        public void Dispose()
        {
            db.Dispose();
        }
    }
}