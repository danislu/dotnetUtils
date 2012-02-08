namespace System.Net
{
    using System.IO;
    using System.Reflection;
    using System.Text;

    public static class AssemblyExtensions
    {
        public static string GetResourceTextfile(this Assembly assembly, string resourceName, Encoding encoding)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new FileLoadException(resourceName);
                }
 
                using (var reader = new StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}