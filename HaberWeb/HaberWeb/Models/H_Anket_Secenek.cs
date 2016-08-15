namespace HaberWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class H_Anket_Secenek
    {
        [Key]
        public int H_A_S_ID { get; set; }

        public int? H_A_S_AnketID { get; set; }

        public string H_A_S_Metin { get; set; }

        public int? H_A_S_OySayisi { get; set; }

        public int? H_A_S_Sirasi { get; set; }

        public virtual H_Anket H_Anket { get; set; }
    }
}
