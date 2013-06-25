using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Extensions
    {
        #region String-Related

        public static bool IsBlank(this string str)
        {
            return str == null || string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
        }

        public static bool IsNotBlank(this string str)
        {
            return !str.IsBlank();
        }

        public static int? TryParseInt(this string str)
        {
            int result;
            if (int.TryParse(str, out result))
                return result;
            return null;
        }

        #endregion
    }
}
