using Cavern;
using System.Collections.Generic;

namespace TestProject1 {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void TestMethod1() {
            Assert.AreEqual(Parser.CanonicalCommand.Move, Parser.Parse("n") );
        }
        [TestMethod]
        public void TestMethod2() {
            Assert.AreEqual(Parser.CanonicalCommand.Move, Parser.Parse("North"));
        }
        [TestMethod]
        public void TestMethod3() {
            Assert.AreEqual(Parser.CanonicalCommand.Unknown, Parser.Parse("NorthEast"));
        }
        [TestMethod]
        public void TestMethod4() {
            Assert.AreEqual(Parser.CanonicalCommand.Look, Parser.Parse("LOOK"));
        }
        [TestMethod]
        public void TestMethod5() {
            Assert.AreEqual(Parser.CanonicalCommand.Unknown, Parser.Parse("LOOKY"));
        }

    }
}