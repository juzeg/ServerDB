#region

using System.Diagnostics.Eventing.Reader;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace Server_Test
{
    [TestClass]
    public class ProgramTests
    {

        [TestMethod]
        public void AdditionTest_4p5_e9()
        {
            Assert.Equals(9, Addition(4, 5));

        }
        [TestMethod]
        public void EnumInterpretatorTest_Create_1()
        {
            Assert.Equals(Interprete("Create"), 1);

        }
        [TestMethod]
        public void SessionClassTest_NameProperty()
        {


            var session = new Session();

            session.name = "test";

            Assert.Equals(session.name, "test");



        }
        
        

        
    }
}