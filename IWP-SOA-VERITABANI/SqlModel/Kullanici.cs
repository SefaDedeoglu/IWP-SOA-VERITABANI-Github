//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IWP_SOA_VERITABANI.SqlModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Kullanici
    {
        public int kullaniciID { get; set; }
        public string KullaniciAd { get; set; }
        public string KullaniciMail { get; set; }
        public string KullaniciPW { get; set; }
        public Nullable<int> soruID { get; set; }
        public Nullable<int> cevapID { get; set; }
    
        public virtual Cevaplar Cevaplar { get; set; }
        public virtual Sorular Sorular { get; set; }
    }
}
