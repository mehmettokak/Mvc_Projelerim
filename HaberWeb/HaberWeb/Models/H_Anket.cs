namespace HaberWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class H_Anket
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public H_Anket()
        {
            H_Anket_Secenek = new HashSet<H_Anket_Secenek>();
        }

        [Key]
        public int H_Anket_ID { get; set; }

        public string H_Anket_Baslik { get; set; }

        public int? H_Anket_KategoriID { get; set; }

        public int? H_ANket_KatilimciSayisi { get; set; }

        public DateTime H_Anket_YayimTarihi { get; set; }

        public DateTime H_Anket_SonOyTarihi { get; set; }

        public bool H_Anket_Aktifmi { get; set; }

        public virtual H_Kategori H_Kategori { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<H_Anket_Secenek> H_Anket_Secenek { get; set; }
    }
}
