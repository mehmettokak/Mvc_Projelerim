namespace HaberWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class H_Yazar
    {
        [Key]
        public Guid H_Y_ID { get; set; }

        public string H_Y_Adi { get; set; }

        public string H_Y_Soyadi { get; set; }

        public string H_Y_ResimYol { get; set; }

        [StringLength(50)]
        public string H_Y_Telefon { get; set; }

        [StringLength(100)]
        public string H_Y_Mail { get; set; }

        public int? H_Y_KategoriID { get; set; }

        public virtual aspnet_Users aspnet_Users { get; set; }

        public virtual H_Kategori H_Kategori { get; set; }
    }
}
