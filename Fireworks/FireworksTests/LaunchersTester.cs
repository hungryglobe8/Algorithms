using Fireworks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FireworksTests
{
    [TestClass]
    public class LaunchersTester
    {
        [TestMethod]
        public void Init()
        {
            Launchers sut = new Launchers();

            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void TwoLaunchers()
        {
            Launchers sut = new Launchers(new List<int> { 5 });

            bool result = sut.LaunchIsPossible(4, 2);
            Assert.IsTrue(result);

            result = sut.LaunchIsPossible(5, 2);
            Assert.IsTrue(result);

            result = sut.LaunchIsPossible(6, 2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SimpleDecision()
        {
            Launchers sut = new Launchers(new List<int> { 1, 2, 3 });

            bool result = sut.LaunchIsPossible(3, 3);
            Assert.IsTrue(result);

            result = sut.LaunchIsPossible(2, 3);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SimpleDecisionMixed()
        {
            Launchers sut = new Launchers(new List<int> { 1, 3, 2 });

            bool result = sut.LaunchIsPossible(3, 2);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SimpleDecisionDescending()
        {
            Launchers sut = new Launchers(new List<int> { 3, 2, 1 });

            bool result = sut.LaunchIsPossible(3, 3);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenTest1()
        {
            Launchers sut = new Launchers(new List<int> { 1, 2, 3 });

            int result = sut.LauncherDistance(1, 3);

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void GivenTest2()
        {
            Launchers sut = new Launchers(new List<int> { 1, 3, 5, 4, 2, 2 });

            int test1 = sut.LauncherDistance(1, 3);
            int test2 = sut.LauncherDistance(1, 5);
            int test3 = sut.LauncherDistance(1, 6);

            Assert.AreEqual(8, test1);
            Assert.AreEqual(4, test2);
            Assert.AreEqual(2, test3);
        }

        [TestMethod]
        public void GivenTest3()
        {
            Launchers sut = new Launchers(new List<int> { 1, 1, 1, 1, 1, 500000000, 1, 1, 1, 1, 1 });

            int result = sut.LauncherDistance(1, 12);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenTest3_Long()
        {
            Launchers sut = new Launchers(new List<int> { 1, 1, 1, 1, 1, 500000000, 1, 1, 1, 1, 1 });

            int result = sut.LauncherDistance(1, 2);

            Assert.AreEqual(500000010, result);
        }

        [TestMethod]
        public void Optimize_Lookup()
        {
            Launchers sut = new Launchers(new List<int> { 1, 3, 5, 4, 2, 2 });

            Run.SaveResultsOptimization(new List<int> { 3, 5, 6 }, sut);
        }
    }
}
