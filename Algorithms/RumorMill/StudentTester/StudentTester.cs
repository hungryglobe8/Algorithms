using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RumorMill;

namespace StudentTester
{
    [TestClass]
    public class StudentTester
    {
        [TestMethod]
        public void Init()
        {
            Student sut = new Student("Dan");

            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void AddFriend()
        {
            Student sut = new Student("Dan");
            Student friend = new Student("Art");

            sut.MakeConnection(friend);

            Assert.IsTrue(sut.Friends.Count == 1);
            Assert.IsTrue(friend.Friends.Count == 1);
        }

        [TestMethod]
        public void AdjacencyMatrix()
        {
            IList<Student> sut = FourStudents();
            int count = 0;

            foreach(Student student in sut)
            {
                count += student.Friends.Count;
            }

            Assert.AreEqual(6, count);
        }

        [TestMethod]
        public void FriendsAreSortedLexicographically()
        {
            Student sut = new Student("Dan");

            sut.MakeConnection(new Student("Zack"));
            sut.MakeConnection(new Student("Jeremy"));
            sut.MakeConnection(new Student("Stewart"));
            sut.MakeConnection(new Student("Chris"));

            IList<Student> result = sut.Friends.Values;
            Assert.AreEqual("Chris", result[0].Name);
            Assert.AreEqual("Jeremy", result[1].Name);
            Assert.AreEqual("Zack", result[3].Name);
        }

        private IList<Student> FourStudents()
        {
            Student a = new Student("Art");
            Student b = new Student("Bea");
            Student d = new Student("Dan");
            Student e = new Student("Edy");

            d.MakeConnection(a);
            d.MakeConnection(b);
            b.MakeConnection(e);

            return new List<Student>() { a, b, d, e };
        }
    }
}
