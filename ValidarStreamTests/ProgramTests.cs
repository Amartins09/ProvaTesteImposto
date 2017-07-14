using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ValidarStream.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void FirstCharTest_input_aAbBABacfe()
        {
            given:
            String input = "aAbBABacfe";

            when:
            ValidarString validar = new ValidarString(input);
            char result = Program.FirstChar(validar);

            then:
            Assert.AreEqual('e', result);
        }

        [TestMethod()]
        public void FirstCharTest_input_14aAbBABacfe()
        {
            given:
            String input = "14aAbBABacfe";

            when:
            ValidarString validar = new ValidarString(input);
            char result = Program.FirstChar(validar);

            then:
            Assert.AreEqual('e', result);
        }

        [TestMethod()]
        public void FirstCharTest_input_iAbBABacfe()
        {
            given:
            String input = "iAbBABacfe";

            when:
            ValidarString validar = new ValidarString(input);
            char result = Program.FirstChar(validar);

            then:
            Assert.AreEqual('e', result);
        }

        [TestMethod()]
        public void FirstCharTest_input_aAbBiBacfe()
        {
            given:
            String input = "aAbBiBacfe";

            when:
            ValidarString validar = new ValidarString(input);
            char result = Program.FirstChar(validar);

            then:
            Assert.AreEqual('i', result);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void FirstCharTest_input_empty()
        {
            given:
            String input = "";

            when:
            ValidarString validar = new ValidarString(input);
            Program.FirstChar(validar);
        }

        [TestMethod()]
        public void FirstCharTest_input_aAbBaBacfa()
        {
            given:
            String input = "aAbBaBacfa";

            when:
            ValidarString validar = new ValidarString(input);
            char result = Program.FirstChar(validar);

            then:
            Assert.AreEqual(' ', result);
        }
    }
}