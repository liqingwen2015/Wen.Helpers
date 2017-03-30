namespace Wen.Helpers.Common.Enums
{
    /// <summary>
    /// 字符编码类型枚举
    /// </summary>
    public enum EncodingType
    {
        /// <summary>
        /// ASCII（7 位）字符集的编码
        /// </summary>
        Ascii = 0,

        /// <summary>
        /// 使用 Big Endian 字节顺序的 UTF-16 格式的编码
        /// </summary>
        BigEndianUnicode = 1,

        /// <summary>
        /// 操作系统的当前 ANSI 代码页的编码
        /// </summary>
        Default = 2,

        /// <summary>
        /// 使用 Little-Endian 字节顺序的 UTF-16 格式的编码
        /// </summary>
        Unicode = 3,

        /// <summary>
        /// 使用 Little-Endian 字节顺序的 UTF-32 格式的编码
        /// </summary>
        Utf32 = 4,

        /// <summary>
        /// UTF-7 格式的编码
        /// </summary>
        Utf7 = 5,

        /// <summary>
        /// UTF-8 格式的编码
        /// </summary>
        Utf8 = 6,

        /// <summary>
        /// 简体中文 (GB2312)
        /// </summary>
        Gb2312 = 7,

        /// <summary>
        /// 简体中文 (GB18030)
        /// </summary>
        Gb18030 = 8
    }
}