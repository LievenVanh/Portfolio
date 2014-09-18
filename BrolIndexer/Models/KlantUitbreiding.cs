using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace BrolIndexer.Models
{
    public partial class Klant
    {
        private string naam;

        public string Naam
        {
            get { return this.Familienaam + " " + this.Voornaam; }
        }
    }
}