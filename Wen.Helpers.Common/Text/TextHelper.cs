#region namespaces

using System.Collections.Generic;
using System.Text.RegularExpressions;

#endregion

namespace Wen.Helpers.Common.Text
{
    /// <summary>
    /// 文本助手
    /// </summary>
    public static class TextHelper
    {
        /// <summary>
        /// 统计单词出现的次数
        /// </summary>
        /// <param name="content">文本内容</param>
        /// <returns></returns>
        public static Dictionary<string, int> CountWrods(string content)
        {
            var dict = new Dictionary<string, int>();
            //用正则将文本分解成单词
            var words = Regex.Split(content, @"\W+");

            foreach (var word in words)
            {
                if (!dict.ContainsKey(word))
                {
                    dict[word] = 1;
                }
                else
                {
                    dict[word]++;
                }
            }

            return dict;
        }
    }
}