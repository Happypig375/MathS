﻿using AngouriMath;
using AngouriMath.Core.Numerix;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Common
{
    [TestClass]
    public class OperatorTest
    {
        [TestMethod]
        public void TestEq()
        {
            Assert.IsTrue(MathS.Var("x") == MathS.Var("x"));
        }
        [TestMethod]
        public void TestIneq()
        {
            Assert.IsTrue(MathS.Var("x") != MathS.Var("y"));
        }
        [TestMethod]
        public void TestR()
        {
            Assert.IsTrue(new ComplexNumber(0, 1) == new ComplexNumber(0, (1.0/3) * 3));
        }
        [TestMethod]
        public void TestDP()
        {
            Assert.IsTrue(MathS.FromString("-23").Eval() == -23);
        }
        [TestMethod]
        public void TestDM()
        {
            Assert.IsTrue(MathS.FromString("1 + -1").Eval() == 0);
        }
        [TestMethod]
        public void TestB()
        {
            Assert.IsTrue(MathS.FromString("1 + (-1)").Eval() == 0);
        }
        [TestMethod]
        public void TestMi()
        {
            Assert.IsTrue(MathS.FromString("-i^2").Eval() == 1);
        }
        [TestMethod]
        public void TestMm()
        {
            Assert.IsTrue(MathS.FromString("-1 * -1 * -1").Eval() == -1);
        }
    }
}
