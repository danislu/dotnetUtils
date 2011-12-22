namespace Utils.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class ReadOnlyDictionaryTests
    {
        private ReadOnlyDictionary<string, string> dict;

        [SetUp]
        public void SetUp()
        {
            var dictionary = new Dictionary<string, string>
                           {
                               {"key1", "value1"}, 
                               {"key2", "value2"}
                           };

            this.dict = new ReadOnlyDictionary<string, string>(dictionary);
        }

        [Test]
        public void ChangingStateShouldNotBeSupported()
        {
            Assert.IsTrue(dict.IsReadOnly);
            Assert.Throws<NotSupportedException>(() => dict.Add("key3", "value3"));
            Assert.Throws<NotSupportedException>(() => dict.Remove("key1"));
            Assert.Throws<NotSupportedException>(() => dict.Clear());
            Assert.Throws<NotSupportedException>(() => dict["key1"] = "something");

            Assert.IsTrue(dict.Keys.IsReadOnly);
            Assert.Throws<NotSupportedException>(() => dict.Keys.Add("something"));
            Assert.Throws<NotSupportedException>(() => dict.Keys.Remove("something"));
            Assert.Throws<NotSupportedException>(() => dict.Keys.Clear());

            Assert.IsTrue(dict.Values.IsReadOnly);
            Assert.Throws<NotSupportedException>(() => dict.Values.Add("something"));
            Assert.Throws<NotSupportedException>(() => dict.Values.Remove("something"));
            Assert.Throws<NotSupportedException>(() => dict.Values.Clear());
        }
    }
}
