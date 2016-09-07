using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRP.Framework
{
    /// <summary>
    /// 类型转化
    /// </summary>
    public static class MyConvert
    {
        public static double ToDouble(this string s)
        {
            double _d=0;
            Double.TryParse(s, out _d);
            return _d;
        }

        public static int ToInt(this string s)
        {
            int _d = 0;
            Int32.TryParse(s, out _d);
            return _d;
        }

        public static Decimal ToDecimal(this string s)
        {
            Decimal _d = 0;
            decimal.TryParse(s, out _d);              
            return _d;
        }

        public static DateTime? ToDate(this string s)
        {
            if (s == null) return null;
            var dt=DateTime.Now;
            if (DateTime.TryParse(s, out dt))
                return dt;
            else
                return null;
        }

       

        public static Guid? ToGuid(this string s)
        {
            if (string.IsNullOrEmpty(s)) return null;
            Guid _d = Guid.Empty;
            if (Guid.TryParse(s, out _d))
                return _d;
            return null;
        }

        /// <summary>
        /// 转化为Boolen
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ToBoolen(this string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            if (s == "1" || s.ToLower().Equals("true"))
                return true;
            return false;
        }
    }
}
