//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BrolIndexer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Status
    {
        public Status()
        {
            this.Herstelbonnen = new HashSet<Herstelbon>();
        }
    
        public string StatusNaam { get; set; }
        public int StatusId { get; set; }
    
        public virtual ICollection<Herstelbon> Herstelbonnen { get; set; }
    }
}
