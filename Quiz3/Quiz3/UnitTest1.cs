
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quiz3
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SortedA()
        {
            int[] A = new int[] { 1, 2, 3, 4, 5 };
            int[] B = new int[] { 6, 7, 8, 9, 10 };

            int result = Select(A, B, 2);

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void SortedB()
        {
            int[] A = new int[] { 6, 7, 8, 9, 10 };
            int[] B = new int[] { 1, 2, 3, 4, 5 };

            int result = Select(A, B, 2);

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void SortedA_DeepElement()
        {
            int[] A = new int[] { 1, 2, 3, 4, 5 };
            int[] B = new int[] { 6, 7, 8, 9, 10 };

            int result = Select(A, B, 8);

            Assert.AreEqual(9, result);
        }

        [TestMethod]
        public void SortedB_DeepElement()
        {
            int[] A = new int[] { 6, 7, 8, 9, 10 };
            int[] B = new int[] { 1, 2, 3, 4, 5 };

            int result = Select(A, B, 8);

            Assert.AreEqual(9, result);
        }

        [TestMethod]
        public void ZeroElement()
        {
            int[] A = new int[] { 6, 7, 8, 9, 10 };
            int[] B = new int[] { 1, 2, 3, 4, 5 };

            int result = Select(A, B, 0);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void MixedArray()
        {
            int[] A = new int[] { 2, 6, 8, 9, 10 };
            int[] B = new int[] { 1, 2, 3, 10, 15 };

            int result = Select(A, B, 0);
            int result1 = Select(A, B, 1);
            int result2 = Select(A, B, 2);

            Assert.AreEqual(1, result);
            Assert.AreEqual(2, result1);
            Assert.AreEqual(2, result2);
        }

        [TestMethod]
        public void MixedArray_Deep()
        {
            int[] A = new int[] { 2, 6, 8, 9, 10 };
            int[] B = new int[] { 1, 2, 3, 10, 15, 22, 40 };

            int result = Select(A, B, 6);
            int result1 = Select(A, B, 8);
            int result2 = Select(A, B, 10);

            Assert.AreEqual(9, result);
            Assert.AreEqual(10, result1);
            Assert.AreEqual(22, result2);
        }

        // A and B are each sorted into ascending order, and 0 <= k < |A|+|B|
        // Returns the element that would be stored at index k if A and B were
        // combined into a single array that was sorted into ascending order.
        private int Select(int[] A, int[] B, int k)
        {
            return Select(A, 0, A.Length - 1, B, 0, B.Length - 1, k);
        }

        private int Select(int[] A, int loA, int hiA, int[] B, int loB, int hiB, int k)
        {
            // A[loA..hiA] is empty
            if (hiA < loA)
                return B[k - loA];
            // B[loB..hiB] is empty
            if (hiB < loB)
                return A[k - loB];
            // Get the midpoints of A[loA..hiA] and B[loB..hiB]
            int i = (loA + hiA) / 2;
            int j = (loB + hiB) / 2;
            // Figure out which one of four cases apply
            if (A[i] > B[j])
            {
                if (k <= i + j)
                    return Select(A, loA, i - 1, B, loB, hiB, k);
                else
                    return Select(A, loA, hiA, B, j + 1, hiB, k);
            }
            else
            {
                if (k <= i + j)
                    return Select(A, loA, hiA, B, loB, j - 1, k);
                else
                    return Select(A, i + 1, hiA, B, loB, hiB, k);
            }
        }
    }
}
