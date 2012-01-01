namespace Utils
{
    using System;
    using System.Collections.Generic;

    public class LambdaEqualityComparer<T> : EqualityComparer<T>
    {
        private readonly Func<T, T, bool> equalsFunc;
        private readonly Func<T, int> getHashCodeFunc;

        public LambdaEqualityComparer(Func<T, T, bool> equalsFunc, Func<T, int> getHashCodeFunc)
        {
            if (equalsFunc == null)
            {
                throw new ArgumentNullException("equalsFunc");
            }
            this.equalsFunc = equalsFunc;

            if (getHashCodeFunc == null)
            {
                throw new ArgumentNullException("getHashCodeFunc");
            }
            this.getHashCodeFunc = getHashCodeFunc;
        }

        public override bool Equals(T x, T y)
        {
            return equalsFunc(x, y);
        }

        public override int GetHashCode(T obj)
        {
            return getHashCodeFunc(obj);
        }
    }
}
