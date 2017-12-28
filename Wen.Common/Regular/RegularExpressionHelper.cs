#region namespaces

using System.Text.RegularExpressions;

#endregion

namespace Wen.Helpers.Regular
{
    /// <summary>
    /// 正则表达式助手
    /// </summary>
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

        #region 校验数字的表达式

        ////数字
        //Regex reg = new Regex(@"^[0-9]*$");
        ////n位的数字
        //Regex reg = new Regex(@"^\d{n}$");
        ////至少n位的数字
        //Regex reg = new Regex(@"^\d{n,}$");
        ////m-n位的数字
        //Regex reg = new Regex(@"^\d{m,n}$");
        ////零和非零开头的数字
        //Regex reg = new Regex(@"^(0|[1-9][0-9]*)$");
        ////非零开头的最多带两位小数的数字
        //Regex reg = new Regex(@"^([1-9][0-9]*)+(.[0-9]{1,2})?$");
        ////带1-2位小数的正数或负数
        //Regex reg = new Regex(@"^(\-)?\d+(\.\d{1,2})?$");
        ////正数、负数、和小数
        //Regex reg = new Regex(@"^(\-|\+)?\d+(\.\d+)?$");
        ////有两位小数的正实数
        //Regex reg = new Regex(@"^[0-9]+(.[0-9]{2})?$");
        ////有1~3位小数的正实数
        //Regex reg = new Regex(@"^[0-9]+(.[0-9]{1,3})?$");
        ////非零的正整数
        //Regex reg = new Regex(@"^[1-9]\d*$ 或 ^([1-9][0-9]*){1,3}$ 或 ^\+?[1-9][0-9]*$");
        ////非零的负整数
        //Regex reg = new Regex(@"^\-[1-9][]0-9″*$ 或 ^-[1-9]\d*$");
        ////非负整数
        //Regex reg = new Regex(@"^\d+$ 或 ^[1-9]\d*|0$");
        ////非正整数
        //Regex reg = new Regex(@"^-[1-9]\d*|0$ 或 ^((-\d+)|(0+))$");
        ////非负浮点数
        //Regex reg = new Regex(@"^\d+(\.\d+)?$ 或 ^[1-9]\d*\.\d*|0\.\d*[1-9]\d*|0?\.0+|0$");
        ////非正浮点数
        //Regex reg = new Regex(@"^((-\d+(\.\d+)?)|(0+(\.0+)?))$ 或 ^(-([1-9]\d*\.\d*|0\.\d*[1-9]\d*))|0?\.0+|0$");
        ////正浮点数
        //Regex reg = new Regex(@"^[1-9]\d*\.\d*|0\.\d*[1-9]\d*$ 或 ^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$");
        ////负浮点数
        //Regex reg = new Regex(@"^-([1-9]\d*\.\d*|0\.\d*[1-9]\d*)$ 或 ^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$");
        ////浮点数
        //Regex reg = new Regex(@"^(-?\d+)(\.\d+)?$ 或 ^-?([1-9]\d*\.\d*|0\.\d*[1-9]\d*|0?\.0+|0)$");

        #endregion 校验数字的表达式

        #region 校验字符的表达式

        ////汉字
        //Regex reg = new Regex(@"^[\u4e00-\u9fa5]{0,}$");
        ////英文和数字
        //Regex reg = new Regex(@"^[A-Za-z0-9]+$ 或 ^[A-Za-z0-9]{4,40}$");
        ////长度为3-20的所有字符
        //Regex reg = new Regex(@"^.{3,20}$");
        ////由26个英文字母组成的字符串
        //Regex reg = new Regex(@"^[A-Za-z]+$");
        ////由26个大写英文字母组成的字符串
        //Regex reg = new Regex(@"^[A-Z]+$");
        ////由26个小写英文字母组成的字符串
        //Regex reg = new Regex(@"^[a-z]+$");
        ////由数字和26个英文字母组成的字符串
        //Regex reg = new Regex(@"^[A-Za-z0-9]+$");
        ////由数字、26个英文字母或者下划线组成的字符串
        //Regex reg = new Regex(@"^\w+$ 或 ^\w{3,20}$");
        ////中文、英文、数字包括下划线
        //Regex reg = new Regex(@"^[\u4E00-\u9FA5A-Za-z0-9_]+$");
        ////中文、英文、数字但不包括下划线等符号
        //Regex reg = new Regex(@"^[\u4E00-\u9FA5A-Za-z0-9]+$ 或 ^[\u4E00-\u9FA5A-Za-z0-9]{2,20}$");
        ////可以输入含有^%&’,;=?$\”等字符
        //Regex reg = new Regex(@"[^%&’,;=?$\x22]+");
        ////禁止输入含有~的字符
        //Regex reg = new Regex(@"[^~\x22]+");

        #endregion 校验字符的表达式

        #region 特殊需求表达式

        ////Email地址
        //Regex reg = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        ////域名
        //Regex reg = new Regex(@"[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(/.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+/.?");
        ////InternetURL
        //Regex reg = new Regex(@"[a-zA-z]+://[^\s]* 或 ^http://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$");
        ////手机号码
        //Regex reg = new Regex(@"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$");
        ////电话号码(“XXX-XXXXXXX”、”XXXX-XXXXXXXX”、”XXX-XXXXXXX”、”XXX-XXXXXXXX”、”XXXXXXX”和”XXXXXXXX)
        //Regex reg = new Regex(@"^($$\d{3,4}-)|\d{3.4}-)?\d{7,8}$");
        ////国内电话号码(0511-4405222、021-87888822)
        //Regex reg = new Regex(@"\d{3}-\d{8}|\d{4}-\d{7}");
        ////身份证号(15位、18位数字)
        //Regex reg = new Regex(@"^\d{15}|\d{18}$");
        ////短身份证号码(数字、字母x结尾)
        //Regex reg = new Regex(@"^([0-9]){7,18}(x|X)?$ 或 ^\d{8,18}|[0-9x]{8,18}|[0-9X]{8,18}?$");
        ////帐号是否合法(字母开头，允许5-16字节，允许字母数字下划线)
        //Regex reg = new Regex(@"^[a-zA-Z][a-zA-Z0-9_]{4,15}$");
        ////密码(以字母开头，长度在6~18之间，只能包含字母、数字和下划线)
        //Regex reg = new Regex(@"^[a-zA-Z]\w{5,17}$");
        ////强密码(必须包含大小写字母和数字的组合，不能使用特殊字符，长度在8-10之间)
        //Regex reg = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,10}$");
        ////日期格式
        //Regex reg = new Regex(@"^\d{4}-\d{1,2}-\d{1,2}");
        ////一年的12个月(01～09和1～12)
        //Regex reg = new Regex(@"^(0?[1-9]|1[0-2])$");
        ////一个月的31天(01～09和1～31)
        //Regex reg = new Regex(@"^((0?[1-9])|((1|2)[0-9])|30|31)$");
        ////钱的输入格式：
        ////有四种钱的表示形式我们可以接受:”10000.00″ 和 “10,000.00”, 和没有 “分” 的 “10000” 和 “10,000”
        //Regex reg = new Regex(@"^[1-9][0-9]*$");
        ////这表示任意一个不以0开头的数字，但是，这也意味着一个字符”0″不通过，所以我们采用下面的形式
        //Regex reg = new Regex(@"^(0|[1-9][0-9]*)$");
        ////一个0或者一个不以0开头的数字.我们还可以允许开头有一个负号
        //Regex reg = new Regex(@"^(0|-?[1-9][0-9]*)$");
        ////这表示一个0或者一个可能为负的开头不为0的数字.让用户以0开头好了.把负号的也去掉，因为钱总不能是负的吧.下面我们要加的是说明可能的小数部分
        //Regex reg = new Regex(@"^[0-9]+(.[0-9]+)?$");
        ////必须说明的是，小数点后面至少应该有1位数，所以”10.”是不通过的，但是 “10” 和 “10.2” 是通过的
        //Regex reg = new Regex(@"^[0-9]+(.[0-9]{2})?$");
        ////这样我们规定小数点后面必须有两位，如果你认为太苛刻了，可以这样
        //Regex reg = new Regex(@"^[0-9]+(.[0-9]{1,2})?$");
        ////这样就允许用户只写一位小数。下面我们该考虑数字中的逗号了，我们可以这样
        //Regex reg = new Regex(@"^[0-9]{1,3}(,[0-9]{3})*(.[0-9]{1,2})?$");
        ////1到3个数字，后面跟着任意个 逗号+3个数字，逗号成为可选，而不是必须
        //Regex reg = new Regex(@"^([0-9]+|[0-9]{1,3}(,[0-9]{3})*)(.[0-9]{1,2})?$");
        ////备注：这就是最终结果了，别忘了”+”可以用”*”替代。如果你觉得空字符串也可以接受的话(奇怪，为什么?)最后，别忘了在用函数时去掉去掉那个反斜杠，一般的错误都在这里
        ////xml文件
        //Regex reg = new Regex(@"^([a-zA-Z]+-?)+[a-zA-Z0-9]+\\.[x|X][m|M][l|L]$");
        ////中文字符的正则表达式
        //Regex reg = new Regex(@"[\u4e00-\u9fa5]");
        ////双字节字符
        //Regex reg = new Regex(@"[^\x00-\xff] (包括汉字在内，可以用来计算字符串的长度(一个双字节字符长度计2，ASCII字符计1))");
        ////空白行的正则表达式，可用来删除空白行
        //Regex reg = new Regex(@"\n\s*\r");
        ////HTML标记的正则表达式
        //Regex reg = new Regex(@"<(\S*?)[^>]*>.*?</\1>|<.*? />");// (网上流传的版本太糟糕，上面这个也仅仅能部分，对于复杂的嵌套标记依旧无能为力)
        ////首尾空白字符的正则表达式
        //Regex reg = new Regex(@"^\s*|\s*$或(^\s*)|(\s*$)");// (可以用来删除行首行尾的空白字符(包括空格、制表符、换页符等等)，非常有用的表达式)
        ////腾讯QQ号
        //Regex reg = new Regex(@"[1-9][0-9]{4,}"); //(腾讯QQ号从10000开始)
        ////中国邮政编码
        //Regex reg = new Regex(@"[1-9]\d{5}(?!\d)");// (中国邮政编码为6位数字)
        ////IP地址
        //Regex reg = new Regex(@"\d+\.\d+\.\d+\.\d+");// (提取IP地址时有用)
        ////IP地址
        //Regex reg = new Regex(@"((?:(?:25[0-5]|2[0-4]\\d|[01]?\\d?\\d)\\.){3}(?:25[0-5]|2[0-4]\\d|[01]?\\d?\\d))");

        #endregion 特殊需求表达式
    }
}