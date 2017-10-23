using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingAssignment
{
    [TestClass]
    public class RecordParserTest
    {
        RecordParser parser = new RecordParser();
        String pipeText = $"Johny | Bravo | Male | Red | 3/4/2012";

        [TestMethod]
        public void ParsePipeText()
        {
            var record = parser.ParseRecord(pipeText);

            Assert.IsTrue(pipeText.Equals(record.ToString()));
        }

        [TestMethod]
        public void ParseCommaText()
        {
            var commaText = $"Johny, Bravo, Male, Red, 3/4/2012";
            var record = parser.ParseRecord(commaText);

            Assert.IsTrue(pipeText.Equals(record.ToString()));
        }

        [TestMethod]
        public void ParseSpaceText()
        {
            var spaceText = $"Johny Bravo Male Red 3/4/2012";
            var record = parser.ParseRecord(spaceText);

            Assert.IsTrue(pipeText.Equals(record.ToString()));
        }
    }
}
