//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NiboOFX.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OFX
    {
        public int Id { get; set; }
        public int IdTrnType { get; set; }
        public System.DateTime DtPosted { get; set; }
        public decimal TrNamt { get; set; }
        public string FitId { get; set; }
        public string CheckNum { get; set; }
        public string Memo { get; set; }
    
        public virtual TrnType TrnType { get; set; }
    }
}
