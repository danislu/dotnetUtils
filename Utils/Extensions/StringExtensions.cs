namespace Utils.Extensions
{
    using System.Text;
    using System.IO;

    public static class StringExtensions
    {
        public static byte[] ToEncodedBytesUtf8(this string value)
        {
            return value.ToEncodedBytes(Encoding.UTF8);
        }
        
        public static byte[] ToEncodedBytes(this string value, Encoding encoding)
        {
            return encoding.GetBytes(value);
        }

        public static Stream ToEncodedStreamUtf8(this string value)
        {
            return value.ToEncodedStream(Encoding.UTF8);
        }

        public static Stream ToEncodedStream(this string value, Encoding encoding)
        {
            return new MemoryStream(value.ToEncodedBytes(encoding));
        }
    }
}
