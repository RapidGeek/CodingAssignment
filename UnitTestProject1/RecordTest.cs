using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CodingAssignment
{
    [TestClass]
    public class RecordTest
    {
        RecordGenerator recordGenerator = new RecordGenerator();

        [TestMethod]
        public void EachRandomlyGeneratedRecordIsUnique()
        {
            var firstRecord = recordGenerator.GenerateRandomRecord();
            var secondRecord = recordGenerator.GenerateRandomRecord();

            Assert.AreNotEqual(firstRecord, secondRecord);
        }

        [TestMethod]
        public void RecordToStringOverride()
        {
            var record = recordGenerator.GenerateRandomRecord();
            var idealToStringRepresentation = $"{record.LastName} | {record.FirstName} | {record.Gender} | {record.FavoriteColor} | {record.DateOfBirth.ToString("M/d/yyyy")}";

            Assert.AreEqual(record.ToString(), idealToStringRepresentation);
        }

        [TestMethod]
        public void RecordComparison()
        {
            var recordList = new List<Record>();
            for(int i = 0; i < 10; i++)
            {
                recordList.Add(recordGenerator.GenerateRandomRecord());
            }

            recordList.Sort();

            var isSortedByLastName = true;
            for(int i = 0; i < 9; i++)
            {
                var lastNameCompare = recordList[i].LastName.CompareTo(recordList[i + 1].LastName);
                if(lastNameCompare > 0)
                {
                    isSortedByLastName = false;
                    break;
                }
            }

            Assert.IsTrue(isSortedByLastName);
        }
    }
}
