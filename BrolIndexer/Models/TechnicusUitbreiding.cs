using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrolIndexer.Models
{
    public partial class Technicus
    {
        private string naam;

        public string Naam
        {
            get { return Familienaam + " " + Voornaam; }
        }
    }
}