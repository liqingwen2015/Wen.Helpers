#region

using System.Text.RegularExpressions;

#endregion

namespace Wen.Helpers.Common
{
    public sealed class RegularExpressionHelper
    {
        /// <summary>
        /// 是否合法的手机号
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <returns>如果正则表达式找到匹配项，则为 true；否则，为 false</returns>
        public static bool IsPhone(string input)
        {
            return Regex.IsMatch(input, @"^[ 1][ 358] \d{9}$");
        }

        /// <summary>
        /// 是否合法的身份证号码
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <returns>如果正则表达式找到匹配项，则为 true；否则，为 false</returns>
        public static bool IsIdCard(string input)
        {
            return Regex.IsMatch(input, @"^( ^\d{15}$|^\d{18}$|^\d{17}(\d| X| x)) $", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 是否合法的邮政编码
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <returns>如果正则表达式找到匹配项，则为 true；否则，为 false</returns>
        public static bool IsPostCard(string input)
        {
            return Regex.IsMatch(input, @"^[ 1- 9] \d{5}$");
        }

        /// <summary>
        /// 是否合法的邮箱
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <returns>如果正则表达式找到匹配项，则为 true；否则，为 false</returns>
        public static bool IsEMail(string input)
        {
            return Regex.IsMatch(input, @"^\w+( [-+ .] \w+) *@ \w+( [- .] \w+) *\. \w+( [- .] \w+) *$");
        }
    }
}