using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public class DataCheckHelper
    {
        //IP正则表达式
        private static readonly Regex _ipregex = new Regex(@"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        //new Regex(
        //    @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");
        //特殊字符正则表达式
        private static readonly Regex _Specialregex =
            new Regex(
                "[*,+,-,!,@,$,%,^,&,>,<,~,;,！,￥,（,）,【,】,《,》,\\{,\\},\\],\\[,?,\\(,\\),\",、,|,:,']");
        private static readonly Regex IsNumberregex = new Regex("^[0-9]*$");
        private static readonly Regex Float = new Regex("^(\\-|\\+)?[\\d]{1,10}(\\.[\\d]{1,10})?$");
        private static readonly Regex isMovePhoneNum = new Regex(@"^0?(13[0-9]|15[0123456789]|17[013678]|18[0-9]|14[57])[0-9]{8}$");
        public static bool IsContainsSpecialChar(string s)
        {
            if (s.IndexOf(" ") > -1)
            {
                return true;
            }
            Match m = _Specialregex.Match(s);
            if (m.Success)
            {
                return true;
            }
            return false;
        }
        public static bool IsMovePhoneNum(string s)
        {
            Match m = isMovePhoneNum.Match(s);
            if (m.Success)
            {
                return true;
            }
            return false;
        }
        public static bool IsNumber(string s)
        {
            if (Int32.TryParse(s, out Int32 result))
            {
                return true;
            }
            return false;
        }
        public static bool IsFloat(string s)
        {
            if (Double.TryParse(s, out double result))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     是否为IP
        /// </summary>
        public static bool IsIP(string s)
        {
            return _ipregex.IsMatch(s);
        }

        public static bool IsDateTime(string s)
        {
            if (DateTime.TryParse(s, out DateTime result))
            {
                return true;
            }
            return false;
        }

    }
}
