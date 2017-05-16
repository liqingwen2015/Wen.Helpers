namespace Wen.Helpers.Common.Extend
{
    /// <summary>
    /// 字符编码类型枚举
    /// </summary>
    public enum EncodingType
    {
        /// <summary>
        /// 空
        /// </summary>
        None = 0,

        /// <summary>
        /// ASCII（7 位）字符集的编码
        /// </summary>
        Ascii = 1,

        /// <summary>
        /// 使用 Big Endian 字节顺序的 UTF-16 格式的编码
        /// </summary>
        BigEndianUnicode = 2,

        /// <summary>
        /// 操作系统的当前 ANSI 代码页的编码
        /// </summary>
        Default = 3,

        /// <summary>
        /// 使用 Little-Endian 字节顺序的 UTF-16 格式的编码
        /// </summary>
        Unicode = 4,

        /// <summary>
        /// 使用 Little-Endian 字节顺序的 UTF-32 格式的编码
        /// </summary>
        Utf32 = 5,

        /// <summary>
        /// UTF-7 格式的编码
        /// </summary>
        Utf7 = 6,

        /// <summary>
        /// UTF-8 格式的编码
        /// </summary>
        Utf8 = 7,

        /// <summary>
        /// 简体中文 (GB2312)
        /// </summary>
        Gb2312 = 8,

        /// <summary>
        /// 简体中文 (GB18030)
        /// </summary>
        Gb18030 = 9
    }
}