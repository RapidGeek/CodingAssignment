using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodingAssignment;
using System.Drawing;
using System.Linq;

namespace CodingAssignment
{
    /// <summary>
    /// Summary description for GeneratorTest
    /// </summary>
    [TestClass]
    public class GeneratorTest
    {
        Generator gen = new Generator();

        [TestMethod]
        public void GeneratorRandomStringTestLength()
        {
            var generatedString = gen.GenerateString(lengthOfString: 25);

            Assert.IsTrue(generatedString.Length == 25);
        }

        [TestMethod]
        public void GeneratorRandomStringNoCapitalLetters()
        {
            var generatedString = gen.GenerateString(lengthOfString: 25);
            var noCapitalLetters = true;

            for (char i = 'A'; i <= 'Z'; i++)
            {
                if(generatedString.Contains(i.ToString()))
                {
                    noCapitalLetters = false;
                    break;
                }
            }

            Assert.IsTrue(noCapitalLetters);
        }

        [TestMethod]
        public void GeneratorRandomStringWithCapitalFirstLetter()
        {
            var generatedString = gen.GenerateString(lengthOfString: 25, capitalizeFirstLetter: true);

            var originalLetter = generatedString[0];
            var toUpperLetter = originalLetter.ToString().ToUpper()[0];

            Assert.IsTrue(originalLetter == toUpperLetter);
        }

        [TestMethod]
        public void GeneratorRandomDateIsValid()
        {
            var isValid = true;
            try
            {
                for (int i = 0; i < 10000; i++)
                {
                    gen.GenerateDateOfBirth();
                }
            }
            catch(Exception ex)
            {
                isValid = false;
            }

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void GeneratorRandomGender()
        {
            var femaleCount = 0;
            var maleCount = 0;
            var otherCount = 0;
            for(int i = 0; i < 10000; i++)
            {
                var gender = gen.GenerateGender();

                if(gender.StartsWith("F"))
                {
                    femaleCount++;
                }
                else if (gender.StartsWith("M"))
                {
                    maleCount++;
                }
                else
                {
                    otherCount++;
                }
            }

            Assert.IsTrue(otherCount == 0);
        }

        [TestMethod]
        public void GeneratorRandomColor()
        {
            var color = gen.GenerateColor();
            var knownColorList = Enum.GetValues(typeof(KnownColor)).Cast<KnownColor>().ToList();
            var colorlist = knownColorList.Select(c => c.ToString());

            Assert.IsTrue(colorlist.Contains(color));
        }
    }
}
