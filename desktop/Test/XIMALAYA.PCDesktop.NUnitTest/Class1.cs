using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace XIMALAYA.PCDesktop.NUnitTest
{
    [TestFixture]
    public class NumersFIxture
    {
        [Test]
        public void AddTwoUumbers()
        {
            int a = 1, b = 2;

            int sum = a + b;

            Assert.AreEqual(sum, 3);
        }
        [Test]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DivideByZero()
        {

            int zero = 0;
            int a = 10;

            int infinity = a / zero;

            Assert.Fail("Should have gotten an exception");

        }
        [Test]
        [Ignore("Multiplication is ignored")]
        public void MultiplyTwoNumbers()
        {
            int a = 10, b = 10;
            int product = a * b;

            Assert.AreEqual(2, product);
        }
    }
}
