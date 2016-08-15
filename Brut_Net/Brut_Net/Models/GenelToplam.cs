using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brut_Net.Models
{
    public class GenelToplam:Bilgiler
    {
        public double ToplamNetucret { get; set; }
        public double ToplamBrutucret { get; set; }
        public double ToplamSskisci { get; set; }
        public double ToplamIssizlikisci { get; set; }
        public double ToplamGelirvergisi { get; set; }
        public double ToplamGvmatrahi { get; set; }
        public double ToplamAgi { get; set; }
        public double ToplamKgvm { get; set; }
        public double ToplamDamgaVergisi { get; set; }
    }
}