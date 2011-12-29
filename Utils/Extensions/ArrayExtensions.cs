namespace System
{
    public static class ArrayExtensions
    {
        public static void Clear(this Array array)
        {
            Array.Clear(array, 0, array.Length);
        }
        
        public static T[] Concat<T>(this T[] a1, T[] a2)
        {
            var resultArray = new T[a1.Length + a2.Length];
            a1.CopyTo(resultArray, 0);
            a2.CopyTo(resultArray, a1.Length);
            return resultArray;
        }

        public static void AppendTo<T>(this T[] a1, ref T[] a2)
        {
            var oldLength = a2.Length;
            Array.Resize(ref a2, a2.Length + a1.Length);
            a1.CopyTo(a2, oldLength);
        }
    }
}
