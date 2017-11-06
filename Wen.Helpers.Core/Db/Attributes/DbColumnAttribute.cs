namespace Wen.Helpers.Db.Attributes
{
    /// <summary>
    /// 列
    /// </summary>
    public class DbColumnAttribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        public DbColumnAttribute(string name)
        {
            Name = name;
        }
    }
}
