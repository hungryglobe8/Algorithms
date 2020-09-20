using BankQueue;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankQueueTests
{
    [TestClass]
    public class BankPeopleTester
    {
        private BankPeople sut;

        [TestMethod]
        [TestInitialize]
        public void Init()
        {
            sut = new BankPeople();
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void AddCustomers()
        {
            sut.AddPerson(0, 1000);
            sut.AddPerson(0, 1500);
            Assert.IsTrue(sut.NumCustomers == 2);
        }

        [TestMethod]
        public void RemoveSingleCustomer()
        {
            sut.AddPerson(0, 1000);
            sut.AddPerson(0, 1500);

            int result = sut.MostMoney(0);

            Assert.AreEqual(1500, result);
            Assert.IsTrue(sut.NumCustomers == 1);
        }
        
        [TestMethod]
        public void AttemptRemoveCustomerTooLate()
        {
            sut.AddPerson(0, 1000);
            sut.AddPerson(0, 1500);

            int result = sut.MostMoney(1);

            Assert.AreEqual(0, result);
            Assert.IsTrue(sut.NumCustomers == 2);
        }

        [TestMethod]
        public void TwoWealthyLateCustomers()
        {
            sut.AddPerson(0, 1000);
            sut.AddPerson(1, 2000);
            sut.AddPerson(1, 1500);

            int result = sut.MostMoney(1) + sut.MostMoney(0);

            Assert.AreEqual(3500, result);
            Assert.IsTrue(sut.NumCustomers == 1);
        }

        [TestMethod]
        public void SampleInput1()
        {
            GivenTest1();
            int result = 0;
            // START AT T-1
            for (int i = 3; i >= 0; i--)
            {
                result += sut.MostMoney(i);
            }
            Assert.AreEqual(4200, result);
            Assert.IsTrue(sut.NumCustomers == 1);
        }

        [TestMethod]
        public void SampleInput2()
        {
            GivenTest2();
            int result = 0;
            // START AT T-1
            for (int i = 3; i >= 0; i--)
            {
                result += sut.MostMoney(i);
            }
            Assert.AreEqual(3000, result);
            Assert.IsTrue(sut.NumCustomers == 1);
        }

        private void GivenTest1()
        {
            sut.AddPerson(1, 1000);
            sut.AddPerson(2, 2000);
            sut.AddPerson(2, 500);
            sut.AddPerson(0, 1200);
        }

        private void GivenTest2()
        {
            sut.AddPerson(0, 1000);
            sut.AddPerson(1, 2000);
            sut.AddPerson(1, 500);
        }
    }
}
