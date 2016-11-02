using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Particles.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod()]
        public void GetAngleC_ZeroPoint()
        {
            Assert.AreEqual(1, Particles.Helpers.RandomNumberGenerator.Instance.NextDouble(1,1));
        }
    }
}
