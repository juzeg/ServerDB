using System;
using Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Server_Test
    
{
    [TestClass]
    public class CommandsTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsFalse(false);


        }
        [TestMethod]
        public void testmetod2()
        {
            Assert.IsFalse(!true);
        }
    }
}
