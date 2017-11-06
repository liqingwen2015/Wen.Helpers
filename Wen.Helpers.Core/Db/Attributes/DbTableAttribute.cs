using System;

namespace Wen.Helpers.Db.Attributes
{
    /// <summary>
    /// 表
    /// </summary>
    public class DbTableAttribute : Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        public DbTableAttribute(string name)
        {
            Name = name;
        }
    }
}
