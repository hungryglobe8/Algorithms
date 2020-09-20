using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumberTheory;
using System;

namespace UnitTests
{
    [TestClass]
    public class CalculatorTester
    {
        [TestMethod]
        public void BasicModularExponents()
        {
            Assert.AreEqual(4, Calculator.ModEx(4, 1, 5));
            Assert.AreEqual(2, Calculator.ModEx(4, 8, 7));
            Assert.AreEqual(2, Calculator.ModEx(4, 5, 7));
            Assert.AreEqual(0, Calculator.ModEx(4, 2, 8));
            //Negative is working?
            Assert.AreEqual(2, Calculator.ModEx(-14, 1, 8));
        }

        [TestMethod]
        [DataRow(12, 2, 90, 13)]
        [DataRow(9, 7, 256, 13)]
        [DataRow(1, 5, 117, 19)]
        [DataRow(1, 4, 4, 5)]
        public void AdvancedModularExponents(int expected, int x, int y, int N)
        {
            Assert.AreEqual(expected, Calculator.ModEx(x, y, N));
        }

        [TestMethod]
        [DataRow(355559)]
        [DataRow(355609)]
        [DataRow(355763)]
        [DataRow(358079)]
        public void PrimeOffBy1ModularExponents(int p)
        {
            int expected = 1;
            for (int i = 2; i < 15; i++)
            {
                Assert.AreEqual(expected, Calculator.ModEx(i, p - 1, p));
            }
        }

        [TestMethod]
        [DataRow(23448887, 2, 2, 2)]
        public void VeryHighModularExponents(int expected, int x, int y, int N)
        {
            x = (int)Math.Pow(x, 30) + 37;
            y = (int)Math.Pow(y, 28) - 1283;
            N = (int)Math.Pow(N, 25) + 12394;
            Assert.AreEqual(expected, Calculator.ModEx(x, y, N));
        }

        [TestMethod]
        [DataRow(2, 14, 6)]
        [DataRow(1, 50, 17)]
        [DataRow(4, 12, 20)]
        [DataRow(6, 54, 24)]
        public void BasicGreatestCommonDenominator(int expected, int a, int b)
        {
            Assert.AreEqual(expected, Calculator.GCD(a, b));
        }

        [TestMethod]
        [DataRow(57, 11571, 1767)]
        [DataRow(8, 72, 112)]
        [DataRow(11, 110, 143)]
        public void AdvancedGreatestCommonDenominator(int expected, int a, int b)
        {
            Assert.AreEqual(expected, Calculator.GCD(a, b));
        }

        [TestMethod]
        [DataRow(2, 2, 3)]
        [DataRow(2, 4, 7)]
        [DataRow(5, 5, 8)]
        [DataRow(5, 9, 11)]
        [DataRow(-1, 4, 6)]
        public void BasicModInverse(int expected, int a, int N)
        {
            Assert.AreEqual(expected, Calculator.ModInv(a, N));
        }

        [TestMethod]
        [DataRow(true, 13)]
        [DataRow(true, 47)]
        [DataRow(false, 10)]
        [DataRow(true, 6011)]
        [DataRow(false, 6)]
        public void BasicPrimality(bool expected, int p)
        {
            Assert.AreEqual(expected, Calculator.IsPrime(p));
        }

        [TestMethod]
        [DataRow(true, 3458999)]
        [DataRow(true, 355549)]
        [DataRow(false, 450025736)]
        public void AdvancedPrimality(bool expected, int p)
        {
            Assert.AreEqual(expected, Calculator.IsPrime(p));
        }

        [TestMethod]
        [DataRow(3, 11, 33, 3, 7)]
        [DataRow(2, 7, 14, 5, 5)]
        [DataRow(5, 3, 15, 3, 3)]
        public void BasicKey(int p, int q, int N, int publicExp, int privateExp)
        {
            Tuple<long, int, long> result = Calculator.Key(p, q);
            Assert.AreEqual(N, result.Item1);
            Assert.AreEqual(publicExp, result.Item2);
            Assert.AreEqual(privateExp, result.Item3);
        }

        [TestMethod]
        [DataRow(2147481673, 2147481199, 96, 3, 7)]
        public void HighKey(int p, int q, int N, int publicExp, int privateExp)
        {
            Tuple<long, int, long> result = Calculator.Key(p, q);
            Assert.AreEqual(N, result.Item1);
            Assert.AreEqual(publicExp, result.Item2);
            Assert.AreEqual(privateExp, result.Item3);
        }

        [TestMethod]
        public void QuizCarmichaels()
        {
            Console.WriteLine(Calculator.Carmichaels(561));
            //Console.WriteLine(Calculator.Carmichaels((int)Math.Pow(2, 22)));
            Console.WriteLine(Calculator.Key(13, 17));
            //Console.WriteLine(Calculator.Key(3, 667));
        }
    }
}
