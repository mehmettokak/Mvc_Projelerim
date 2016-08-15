namespace HaberWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class H_Yorum
    {
        [Key]
        public int H_Y_ID { get; set; }

        public int? H_Y_HaberID { get; set; }

        public int? H_Y_GorusID { get; set; }

        public string H_Y_Baslik { get; set; }

        [StringLength(50)]
        public string H_Y_Ip { get; set; }

        [StringLength(50)]
        public string H_Y_AdSoyad { get; set; }

        [StringLength(100)]
        public string H_Y_Mail { get; set; }

        public string H_Y_Icerik { get; set; }

        public DateTime H_Y_YayimTarihi { get; set; }

        public bool? H_Y_Onay { get; set; }

        public int H_Y_Begendim { get; set; }

        public int H_Y_Begenmedim { get; set; }

        public virtual H_Gorusler H_Gorusler { get; set; }

        public virtual Haber Haber { get; set; }
    }
}
