using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serwer2;

namespace Server.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void ConnectionSetupTest_DefaultValue_127001()
        {
          var connection = Program.ConnectionSetup("d");
            Assert.IsTrue(connection.IP == "127.0.0.1" );

        }
        [TestMethod()]
        public void ConnectionSetupTest_GivenIP_IP()
        {
            var connection = Program.ConnectionSetup("1.1.1.1");
            Assert.IsTrue(connection.IP == "1.1.1.1");

        }
        [TestMethod()]
        public void ConnectionSetupTest_GivenIP2_GivenIP2()
        {
            var connection = Program.ConnectionSetup("23.2.3.3");
            Assert.IsTrue(connection.IP == "23.2.3.3");

        }
        [TestMethod()]
        public void ConnectionSetupTest_RandomValue_Null()
        {
            var connection = Program.ConnectionSetup("123213");
            Assert.IsTrue(null == connection);

        }
    }
}