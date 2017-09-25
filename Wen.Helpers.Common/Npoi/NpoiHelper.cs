#region namespaces

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

#endregion

namespace Wen.Helpers.Common.Npoi
{
    /// <summary>
    /// Npoi 助手
    /// </summary>
    public class NpoiHelepr
    {
        /// <summary>
        /// 写入到 Excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sheetName"></param>
        /// <param name="columnNames"></param>
        /// <param name="data"></param>
        /// <param name="filePath"></param>
        /// <remarks>
        /// HSSFWorkbook:是操作Excel2003以前（包括2003）的版本，扩展名是.xls；
        /// XSSFWorkbook:是操作Excel2007的版本，扩展名是.xlsx；
        /// </remarks>
        public static void WriteExcel<T>(string sheetName, IEnumerable<string> columnNames, IEnumerable<T> data,
            string filePath)
            where T : class, new()
        {
            if (string.IsNullOrEmpty(sheetName))
                throw new Exception("sheetName 不能为空");

            if (string.IsNullOrEmpty(filePath))
                throw new Exception("filePath 不能为空");

            var dataArr = data as T[] ?? data.ToArray();
            if (!dataArr.Any())
                return;

            var names = columnNames.ToArray();
            var book = new XSSFWorkbook();
            var sheet = book.CreateSheet(sheetName);
            var row = sheet.CreateRow(0);

            PropertyInfo[] props = null;

            for (var i = 0; i < names.Length; i++)
                row.CreateCell(i).SetCellValue(names[i]);

            for (var i = 0; i < dataArr.Length; i++)
            {
                var row2 = sheet.CreateRow(i + 1);

                var obj = dataArr[i];

                if (props == null)
                {
                    var type = obj.GetType();
                    props = type.GetProperties();
                }

                var j = 0;
                foreach (var prop in props)
                {
                    row2.CreateCell(j).SetCellValue(Convert.ToString(prop.GetValue(obj)));
                    j++;
                }
            }

            WriteFile(book, filePath);
        }

        /// <summary>
        /// 写入到客户端
        /// </summary>
        /// <param name="book"></param>
        /// <param name="filePath"></param>
        private static void WriteFile(IWorkbook book, string filePath)
        {
            using (var ms = new MemoryStream())
            {
                book.Write(ms);
                using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    var data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }
    }
}