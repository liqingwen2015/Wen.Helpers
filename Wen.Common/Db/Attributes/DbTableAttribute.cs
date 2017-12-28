using System;

namespace Wen.Common.Db.Attributes
{
    /// <inheritdoc />
    /// <summary>
    /// 表
    /// </summary>
    public class DbTableAttribute : Attribute
    {
        public DbTableAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}