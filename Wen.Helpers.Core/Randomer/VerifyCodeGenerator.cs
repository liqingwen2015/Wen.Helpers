using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wen.Helpers.Randomer
{
    /// <summary>
    /// 验证码生成器
    /// </summary>
    public class VerifyCodeGenerator
    {
        private static readonly char[] Characters = { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        public static string GenerateVerifyCode(int length)
        {
            var chars = new int[length];

            for (var i = 0; i < length; i++)
            {
                chars[i] = RandomGenerator.Next(Characters.Length);
            }

            return string.Join("", chars);
        }
    }
}
