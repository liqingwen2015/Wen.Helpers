using System;
using Wen.Common.Extensions;

namespace Wen.Common.Randomer
{
    /// <summary>
    /// 验证码生成器
    /// </summary>
    public class VerifyCodeGenerator
    {
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        public static string GenerateVerifyCode(int length)
        {
            var chars = new char[length];
            var characters = new[]
            {
                '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y'
            };

            for (var i = 0; i < length; i++)
                chars[i] = characters[RandomGenerator.Next(characters.Length)];

            return chars.ToJoinInString("");
        }
    }
}