using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jettsonator.Combinators;

namespace Tests
{
    [TestClass]
    public class CombinatorTests
    {
        [TestMethod]
        public void KCombinatorIEnumberableTest()
        {
            List<int> list = new List<int>();
            list.Add(10);
            list.Add(23523);
            list.Add(23432);

            Type expected = list[0].GetType();
            list.K((int actual) => Assert.IsInstanceOfType(actual, expected));
        }

        [TestMethod]
        public void kCombinatorNonEnumerableTest()
        {
            double test = 12.54;

            test.k(actual => Assert.AreEqual(actual, test));

        }

        [TestMethod]
        public void BCombinatorIEnumerableTest()
        {
            List<int> original = new List<int>();

            original.Add(10);
            original.Add(52);
            original.Add(100);
            original.Add(-5);

            Func<int, int> x = i => i + 1;
            Func<int, int> y = i => i + 1;

            List<int> actual = original.B(x, y);

            for (int i = 0; i < original.Count; i++)
            {
                Assert.AreEqual(x(y(original[i])), actual[i]);
            }
        }

        [TestMethod]
        public void bCombinatorNonEnumerableTest()
        {
            int original = 10;
            Func<int, int> x = i => i + 1;
            Func<int, int> y = i => i + 1;

            int actual = original.b(x, y);

            Assert.AreEqual(x(y(original)), actual);
        }

        [TestMethod]
        public void CCombinatorIEnumerableTest()
        {
            List<int> original = new List<int>();

            original.Add(15);
            original.Add(152);
            original.Add(235);
            original.Add(23);

            Func<int, int, int> x = (a,b) => a + b;
            int y = 10;

            List<int> actual = original.C(x, y);

            for(int i=0; i < original.Count; i++)
            {
                Assert.AreEqual(x(y, original[i]), actual[i]);
            }
        }

        [TestMethod]
        public void cCombinatorNonEnumerable()
        {
            int original = 20;

            Func<int, int, int> x = (a, b) => a + b;
            int y = 10;

            int actual = original.c(x, y);

            Assert.AreEqual(x(y, original), actual);
        }
    }
}
