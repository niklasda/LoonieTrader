using System;
using LoonieTrader.Library.Extensions;

namespace LoonieTrader.Library.Tests.Extensions
{
    [TestClass]
    public class MathExTests
    {
        [TestMethod]
        public void TestNormalPowers()
        {
            Assert.AreEqual(1, MathEx.IntPow(0, 0));
            Assert.AreEqual(1, MathEx.IntPow(1, 1));
            Assert.AreEqual(1, MathEx.IntPow(1, 2));
            Assert.AreEqual(2, MathEx.IntPow(2, 1));
            Assert.AreEqual(4, MathEx.IntPow(2, 2));
            Assert.AreEqual(9, MathEx.IntPow(3, 2));
            Assert.AreEqual(8, MathEx.IntPow(2, 3));
            Assert.AreEqual(27, MathEx.IntPow(3, 3));
            Assert.AreEqual(1000, MathEx.IntPow(10, 3));
            Assert.AreEqual(1000000, MathEx.IntPow(10, 6));
            Assert.AreEqual(1000000000, MathEx.IntPow(10, 9));
            Assert.AreEqual(1000000000000, MathEx.IntPow(10, 12));
            Assert.AreEqual(1000000000000000, MathEx.IntPow(10, 15));
            Assert.AreEqual(1000000000000000000, MathEx.IntPow(10, 18));
            // Assert.AreEqual(10000000000000000000, MathEx.IntPow(10, 19));
        }

        [TestMethod]
        public void TestNegativeNumbersPowers()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(()=> MathEx.IntPow(-1, 1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(()=> MathEx.IntPow(1, -1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(()=> MathEx.IntPow(-1, -1));
        }
    }
}