namespace Wen.Common.Db.Attributes
{
    /// <summary>
    /// 列
    /// </summary>
    public class DbColumnAttribute
    {
        public DbColumnAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}