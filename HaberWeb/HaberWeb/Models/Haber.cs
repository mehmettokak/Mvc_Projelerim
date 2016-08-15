namespace HaberWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Haber")]
    public partial class Haber
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Haber()
        {
            H_Resim = new HashSet<H_Resim>();
            H_Yorum = new HashSet<H_Yorum>();
            H_Etiket = new HashSet<H_Etiket>();
        }

        [Key]
        public int H_id { get; set; }

        public string H_Baslik { get; set; }

        public string H_ozet { get; set; }

        public string H_icerik { get; set; }

        public DateTime H_YayimTarihi { get; set; }

        public Guid? H_YazarID { get; set; }

        public int? H_KategoriID { get; set; }

        public int? H_TipID { get; set; }

        public string H_ResimYol { get; set; }

        public string H_KucukResimYol { get; set; }

        public int? H_Goruntuleme { get; set; }

        public string H_VideoYol { get; set; }

        public virtual aspnet_Users aspnet_Users { get; set; }

        public virtual H_Kategori H_Kategori { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<H_Resim> H_Resim { get; set; }

        public virtual H_Tip H_Tip { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<H_Yorum> H_Yorum { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<H_Etiket> H_Etiket { get; set; }
    }
}
