using Microsoft.VisualStudio.TestTools.UnitTesting;
using RumorMill;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestClass]
    public class SchoolTester
    {
        [TestMethod]
        public void Init()
        {
            School sut = new School(BalancedTree());

            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void BalancedTreeTest()
        {
            IList<Student> list = BalancedTree();
            School sut = new School(list);

            sut.GenerateReports(new List<Student>() { list[0] });
        }

        [TestMethod]
        public void UnbalancedTreeTest()
        {
            IList<Student> list = UnbalancedTree();
            School sut = new School(list);

            sut.GenerateReports(new List<Student>() { list[0] });
        }

        private IList<Student> BalancedTree()
        {
            Student a = new Student("A");
            Student b = new Student("B");
            Student c = new Student("C");
            Student d = new Student("D");
            Student e = new Student("E");
            Student f = new Student("F");
            Student g = new Student("G");

            a.MakeConnection(b);
            a.MakeConnection(c);
            b.MakeConnection(d);
            b.MakeConnection(g);
            c.MakeConnection(e);
            c.MakeConnection(f);

            return new List<Student>() { a, b, c, d, e, f, g };
        }

        private IList<Student> UnbalancedTree()
        {
            Student a = new Student("A");
            Student b = new Student("B");
            Student c = new Student("C");
            Student d = new Student("D");
            Student e = new Student("E");
            Student f = new Student("F");
            Student g = new Student("G");
            Student x = new Student("X");
            Student y = new Student("Y");
            Student z = new Student("Z");

            a.MakeConnection(b);
            a.MakeConnection(x);
            b.MakeConnection(c);
            c.MakeConnection(d);
            d.MakeConnection(e);
            d.MakeConnection(y);
            e.MakeConnection(f);
            f.MakeConnection(g);
            z.MakeConnection(f);

            return new List<Student>() { a, z, b, c, x, y, d, e, f, g };
        }
    }
}
