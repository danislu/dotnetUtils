namespace System.IO
{
    public static class StreamExtensions
    {
        public static string ReadString(this Stream stream)
        {
            using (var sr = new StreamReader(stream))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
