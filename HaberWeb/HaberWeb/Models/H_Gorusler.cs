namespace HaberWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class H_Gorusler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public H_Gorusler()
        {
            H_Yorum = new HashSet<H_Yorum>();
        }

        [Key]
        public int H_G_id { get; set; }

        public string H_G_Baslik { get; set; }

        public string H_G_Icerik { get; set; }

        public DateTime H_G_Tarih { get; set; }

        public Guid? H_G_YazarID { get; set; }

        public int? H_G_Begendim { get; set; }

        public int? H_G_Begenmedim { get; set; }

        public virtual aspnet_Users aspnet_Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<H_Yorum> H_Yorum { get; set; }
    }
}
