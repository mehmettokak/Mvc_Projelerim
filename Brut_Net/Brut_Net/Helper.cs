using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brut_Net
{
    public static class Helper
    {
        public static string ToPriceText(this object item)
        {
            string result = "";
            try
            {
                result = item.ToString() + " TL";
            }
            catch {}
            return result;
        }
    }
}