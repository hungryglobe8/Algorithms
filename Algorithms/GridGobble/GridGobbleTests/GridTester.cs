using GridGobble;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GridGobbleTests
{
    [TestClass]
    public class GridTester
    {
        [TestMethod]
        public void Init()
        {
            Grid sut = new Grid(0);

            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void AddRow()
        {
            Grid sut = new Grid(3);

            sut.AddRow(new List<int> { 1, 1, 1 });

            Assert.IsTrue(sut.NumRows == 1);
        }

        [TestMethod]
        public void AddMultipleRows()
        {
            Grid sut = new Grid(3);

            sut.AddRow(new List<int> { 1, 1, 1 });
            sut.AddRow(new List<int> { 1, 1, 1 });

            Assert.IsTrue(sut.NumRows == 2);
        }

        [TestMethod]
        public void GetRow()
        {
            Grid sut = new Grid(3);
            sut.AddRow(new List<int> { 1, 2, 3 });

            int[] result = sut.GetRow(0);

            Assert.IsTrue(result[0] == 1);
            Assert.IsTrue(result[1] == 2);
            Assert.IsTrue(result[2] == 3);
        }

        [TestMethod]
        public void GetMaxValue_2x2()
        {
            Grid sut = new Grid(2);
            sut.AddRow(new List<int> { 1, 1 });
            sut.AddRow(new List<int> { 5, 10 });

            int result = sut.GetMaxValue();

            Assert.AreEqual(11, result);
        }

        [TestMethod]
        public void GetMaxValue_2x3()
        {
            Grid sut = new Grid(2);
            sut.AddRow(new List<int> { 1, 10 });
            sut.AddRow(new List<int> { 1, 1 });
            sut.AddRow(new List<int> { 4, 1 });

            int result = sut.GetMaxValue();

            Assert.AreEqual(13, result);
        }

        [TestMethod]
        public void SampleInput1()
        {
            Grid sut = new Grid(3);
            sut.AddRow(new List<int> { 3, 5, 8 });
            sut.AddRow(new List<int> { 3, 1, 2 });
            sut.AddRow(new List<int> { 4, 9, 4 });

            int result = sut.GetMaxValue();

            Assert.AreEqual(16, result);
        }

        [TestMethod]
        public void SampleInput2()
        {
            Grid sut = new Grid(4);
            sut.AddRow(new List<int> { 1, 2, 3, 9 });
            sut.AddRow(new List<int> { 3, 7, 6, 1 });
            sut.AddRow(new List<int> { 9, 2, 1, 3});

            int result = sut.GetMaxValue();

            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void SampleInput3()
        {
            Grid sut = new Grid(4);
            sut.AddRow(new List<int> { 1, 1, 1, 10000});
            sut.AddRow(new List<int> { 1, 1, 1, 1 });
            sut.AddRow(new List<int> { 10000, 1, 1, 1 });
            sut.AddRow(new List<int> { 1, 1, 1, 1 });
            sut.AddRow(new List<int> { 1, 1, 1, 10000 });

            int result = sut.GetMaxValue();

            Assert.AreEqual(29998, result);
        }


    }
}
