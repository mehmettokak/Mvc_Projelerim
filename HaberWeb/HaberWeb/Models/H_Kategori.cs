namespace HaberWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class H_Kategori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public H_Kategori()
        {
            H_Anket = new HashSet<H_Anket>();
            H_Kategori1 = new HashSet<H_Kategori>();
            H_Yazar = new HashSet<H_Yazar>();
            Haber = new HashSet<Haber>();
        }

        [Key]
        public int H_K_ID { get; set; }

        public string H_K_Adi { get; set; }

        public string H_K_Aciklama { get; set; }

        public string H_K_ResimYol { get; set; }

        public int? H_K_UstkategoriID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<H_Anket> H_Anket { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<H_Kategori> H_Kategori1 { get; set; }

        public virtual H_Kategori H_Kategori2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<H_Yazar> H_Yazar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Haber> Haber { get; set; }
    }
}
