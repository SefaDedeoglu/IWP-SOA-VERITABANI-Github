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
    using System.ComponentModel;
    using System.Web;
    public partial class Sorular
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sorular()
        {
            this.Cevaplars = new HashSet<Cevaplar>();
        }
    
        public int soruID { get; set; }
        public string soruBaslik { get; set; }
        public string soruOzet { get; set; }
        public string soruIcerik { get; set; }
        public string soruResim { get; set; }
        public Nullable<System.DateTime> soruTarih { get; set; }
        public Nullable<int> soruGorunme { get; set; }
        public Nullable<int> soruCevapSayisi { get; set; }
        public Nullable<int> kategoriID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cevaplar> Cevaplars { get; set; }
        public virtual Kategori Kategori { get; set; }

    }
}
