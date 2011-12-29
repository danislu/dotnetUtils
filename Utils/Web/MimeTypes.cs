namespace Utils.Web
{
    /// <summary>
    /// http://www.iana.org/assignments/media-types/index.html
    /// </summary>
    public static class MimeTypes
    {
        public static readonly string Pain = "text/plain";
        public static readonly string Html = "text/html";

        public static readonly string Json = "application/json";

        public static readonly string Xml = "application/xml";
        public static readonly string Rss = "application/rss+xml";
        public static readonly string Atom = "application/atom+xml";
        public static readonly string AtomFeed = Atom + ";type=feed";
        public static readonly string AtomEntry = Atom + ";type=entry";
        public static readonly string AtomCat = "application/atomcat+xml";
        public static readonly string AtomPub = "application/atomsvc+xml";
    }
}
