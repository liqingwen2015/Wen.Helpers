#region namespaces

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

#endregion

namespace Wen.Common.Document
{
    /// <summary>
    /// Csv 助手
    /// </summary>
    public static class CsvHelper
    {
        /// <summary>
        /// 保存 csv 文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="data">数据</param>
        public static bool SaveAsCsv<T>(string fileName, IList<T> data) where T : class, new()
        {
            bool flag;

            try
            {
                var sb = new StringBuilder();
                //通过反射 显示要显示的列
                const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public |
                                                  BindingFlags.Static; //反射标识
                var type = typeof(T);
                var properties = type.GetProperties(bindingFlags);
                var propertyNames = new List<string>();

                for (var i = 0; i < properties.Length; i++)
                {
                    if (!propertyNames.Contains(properties[i].Name))
                        propertyNames.Add(properties[i].Name);

                    sb.Append(properties[i].Name);

                    if (i != properties.Length - 1)
                        sb.Append(",");
                    else
                        sb.AppendLine();
                }

                foreach (var model in data)
                {
                    var linResult = new StringBuilder();

                    foreach (var name in propertyNames)
                    {
                        var prop = type.GetProperty(name);
                        if (prop != null)
                        {
                            var propValue = prop.GetValue(model);
                            var value = (propValue ?? string.Empty).ToString().Trim();
                            if (value.IndexOf(',') != -1)
                                value = "\"" + value.Replace("\"", "\"\"") + "\""; //特殊字符处理 ？

                            if (!string.IsNullOrEmpty(value))
                            {
                                var valueType = prop.PropertyType;

                                if (valueType == typeof(decimal?) || valueType == typeof(decimal))
                                    value = decimal.Parse(value).ToString("#.#");
                                else if (valueType == typeof(double?) || valueType == typeof(double))
                                    value = double.Parse(value).ToString("#.#");
                                else if (valueType == typeof(float?) || valueType == typeof(float))
                                    value = float.Parse(value).ToString("#.#");
                            }

                            linResult.Append(value).Append(",");
                        }
                        else
                        {
                            linResult.Append(",");
                        }

                        break;
                    }

                    sb.AppendLine(linResult.ToString(0, linResult.Length - 1));
                }

                var content = sb.ToString();
                var dir = Directory.GetCurrentDirectory();
                var fullName = Path.Combine(dir, fileName);

                if (File.Exists(fullName)) File.Delete(fullName);

                using (var fs = new FileStream(fullName, FileMode.CreateNew, FileAccess.Write))
                {
                    var sw = new StreamWriter(fs, Encoding.Default);
                    sw.Flush();
                    sw.Write(content);
                    sw.Flush();
                    sw.Close();
                }

                flag = true;
            }
            catch
            {
                flag = false;
            }

            return flag;
        }
    }
}