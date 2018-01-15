using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Wen.Common.Randomer;

namespace Wen.Common.Web.Mvc.VerifyCode
{
    /// <summary>
    /// 验证码助手
    /// </summary>
    public class VerifyCodeHelper
    {
        /// <summary>
        /// 创建验证码图片流
        /// </summary>
        /// <param name="codeText"></param>
        /// <param name="format"></param>
        /// <param name="codeMaxWidth"></param>
        /// <param name="codeMaxHeight"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public static byte[] CreateVerifyCodeStream(string codeText, ImageFormat format, int codeMaxWidth = 80, int codeMaxHeight = 30, int fontSize = 16)
        {
            //颜色列表，用于验证码、噪线、噪点 
            Color[] colors =
            {
                Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue
            };

            //字体列表，用于验证码 
            string[] fonts = { "Times New Roman", "Calibri", "Microsoft YaHei UI", "Arial" };

            //创建画布
            var bmp = new Bitmap(codeMaxWidth, codeMaxHeight);
            var g = Graphics.FromImage(bmp);
            var pen = new Pen(colors[RandomGenerator.Next(colors.Length)]);
            var solidBrush = new SolidBrush(colors[RandomGenerator.Next(colors.Length)]);
            var fontString = fonts[RandomGenerator.Next(fonts.Length)];
            var font = new Font(fontString, fontSize);

            g.Clear(Color.White);

            //画噪线 
            for (var i = 0; i < 5; i++)
            {
                var x1 = RandomGenerator.Next(codeMaxWidth);
                var y1 = RandomGenerator.Next(codeMaxHeight);
                var x2 = RandomGenerator.Next(codeMaxWidth);
                var y2 = RandomGenerator.Next(codeMaxHeight);

                g.DrawLine(pen, x1, y1, x2, y2);
            }

            //画验证码字符串 
            for (var i = 0; i < codeText.Length; i++)
            {
                g.DrawString(codeText[i].ToString(), font, solidBrush, (float)i * 18, 0);
            }

            //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
            var ms = new MemoryStream();

            try
            {
                bmp.Save(ms, format);
                return ms.ToArray();
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                g.Dispose();
                bmp.Dispose();
                pen.Dispose();
                solidBrush.Dispose();
                font.Dispose();
                ms.Dispose();
            }
        }

    }
}
