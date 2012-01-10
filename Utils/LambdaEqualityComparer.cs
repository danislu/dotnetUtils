namespace Utils
{
    using System;
    using System.Collections.Generic;

    public class LambdaEqualityComparer<T> : EqualityComparer<T>
    {
        private readonly Func<T, T, bool> equalsFunc = (x, y) => x.Equals(y);
        private readonly Func<T, int> getHashCodeFunc = x => x.GetHashCode();

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaEqualityComparer&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="equalsFunc">The equals func. Default value is the default for <see cref="T"/></param>
        /// <param name="getHashCodeFunc">The get hash code func. Default value is the default for <see cref="T"/></param>
        public LambdaEqualityComparer(Func<T, T, bool> equalsFunc = null, Func<T, int> getHashCodeFunc = null)
        {
            if (equalsFunc != null)
            {
                this.equalsFunc = equalsFunc;
            }

            if (getHashCodeFunc != null)
            {
                this.getHashCodeFunc = getHashCodeFunc;
            }
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
