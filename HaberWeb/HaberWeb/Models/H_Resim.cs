namespace HaberWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class H_Resim
    {
        [Key]
        public int H_R_ID { get; set; }

        public int? H_R_HaberID { get; set; }

        public string H_R_ResimYol { get; set; }

        public string H_R_KucukResimYol { get; set; }

        public string H_R_Ozet { get; set; }

        public virtual Haber Haber { get; set; }
    }
}
